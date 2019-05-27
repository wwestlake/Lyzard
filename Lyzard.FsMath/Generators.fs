(* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *)
namespace Lyzard.FsMath

open System

module Generators =

    let PI = Math.PI

    let generator f (start:float) (amplitude:float) (frequency:float) (sampleRate:float) (phase: float) : (float * float) seq =
        let w = 2.0 * Math.PI * frequency
        let sampTime = 1.0 / sampleRate
        let sampRad = w * sampTime
        let tstep = 1.0 / sampleRate
        let rec inner time = seq {
            yield (time, f time amplitude frequency phase sampTime sampRad tstep (time * w))
            yield! inner (time + sampTime)
        }
        inner start
    
    let rand = new Random(int(DateTime.Now.Ticks))
    
    let randomFunc _ amplitude _ _ _ _ _ _ =
        rand.NextDouble() * 2.0 - 1.0 * amplitude

    let sineFunc time amplitude frequency phase _ _ _ _ =
        let omega = (2.0 * PI * frequency)
        Math.Sin(omega * time + phase) * amplitude

    let squareFunc time amplitude frequency phase sampTime sampRad tstep theta =
        let value = sineFunc time amplitude frequency phase sampTime sampRad tstep theta
        if value >= 0.0 then amplitude else -amplitude

    let triangleFunc time amplitude frequency phase _ _ _ _ =
        let omega = 2.0 * PI * frequency
        let deltaTime = 1.0 / frequency + (phase / omega)
        let adjustedTime = time + deltaTime
        let cycle = int (frequency * adjustedTime * 4.0)
        let tao = adjustedTime - float(cycle) / 4.0 / frequency
        let cyclePosition = int (frequency * adjustedTime * 4.0) % 4
        let m = 4.0 * amplitude * frequency
        match cyclePosition with
        | 0 -> tao * m
        | 1 -> amplitude - tao * m
        | 2 -> -tao * m
        | _ -> -amplitude + tao * m

    let impulseFunc delaySamples _ _ _ _ sampTime sampRad tstep theta =
        if (theta >= (delaySamples * sampRad)) && (theta < ((delaySamples + 1.0) * sampRad)) then 1.0 else 0.0

    let stepFunc delaySamples _ _ _ _ sampTime sampRad tstep theta =
        if theta > (delaySamples * sampRad) then 1.0 else 0.0


    let randomWave = generator randomFunc
    let sineWave = generator sineFunc
    let squareWave = generator squareFunc
    let triangleWave = generator triangleFunc
    let impulseGenerator delaySamples = generator (impulseFunc delaySamples)
    let stepGenerator delaySamples = generator (stepFunc delaySamples)


    type FunctionGenerator(func) =
        member val Func = func with get, set
        member val StartTime = 0.0 with get, set 
        member val Amplitude = 1.0 with get, set
        member val Frequency = 100.0 with get, set
        member val SampleRate = 44100.0 with get, set
        member val Phase = 0.0 with get, set
        member x.Generate() =
            x.Func x.StartTime x.Amplitude x.Frequency x.SampleRate x.Phase


    type SquareWaveGenerator() =
        inherit FunctionGenerator(squareWave)

    type SineWaveGenerator() =
        inherit FunctionGenerator(sineWave) 

    type TriangleWaveGenerator() =
        inherit FunctionGenerator(triangleWave)

    type RandomWaveGenerator() =
        inherit FunctionGenerator(randomWave)
