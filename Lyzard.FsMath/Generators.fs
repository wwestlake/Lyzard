namespace Lyzard.FsMath

open System

module Generators =

    let PI = Math.PI

    let generatorWithTime f start amplitude frequency sampleRate phase =
        let w = 2.0 * Math.PI * frequency
        let sampTime = 1.0 / sampleRate
        let sampRad = w * sampTime
        let tstep = 1.0 / sampleRate
        let rec inner time = seq {
            yield (time, f time amplitude frequency phase sampTime sampRad tstep (time * w))
            yield! inner (time + sampTime)
        }
        inner start


    let generator f start amplitude frequency sampleRate phase =
        generatorWithTime f start amplitude frequency sampleRate phase
        |> Seq.map (fun x -> snd x)
    
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
    let randomWaveWithTime _ amplitude _ _ _ = generatorWithTime randomFunc
    let sineWave = generator sineFunc
    let sineWaveWithTime = generatorWithTime sineFunc
    let squareWave = generator squareFunc
    let squareWaveWithTime = generatorWithTime squareFunc
    let triangleWave = generator triangleFunc
    let triangleWaveWithTime = generatorWithTime triangleFunc
    let impulseGenerator delaySamples = generator (impulseFunc delaySamples)
    let impulseGeneratorWithTime delaySamples = generatorWithTime (impulseFunc delaySamples)
    let stepGenerator delaySamples = generator (stepFunc delaySamples)
    let stepGeneratorWithTime delaySamples = generatorWithTime (stepFunc delaySamples)

    let toFloatSeq s =
        s |> Seq.map (fun x -> float32(x))

    type FunctionGenerator(func, startTime, amplitude, frequency, sampleRate, phase) =
        member private x.func = func
        member private x.startTime = startTime
        member private x.amplitude = amplitude
        member private x.frequency = frequency
        member private x.sampleRate = sampleRate
        member private x.phase = phase
        member x.Generate() =
            x.func x.startTime x.amplitude x.frequency x.sampleRate x.phase
        member x.GenerateFloat() =
            x.func x.startTime x.amplitude x.frequency x.sampleRate x.phase |> toFloatSeq

    type SineWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(sineWave, startTime, amplitude, frequency, sampleRate, phase)

    type SquareWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(squareWave, startTime, amplitude, frequency, sampleRate, phase)

    type TriangleWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(triangleWave, startTime, amplitude, frequency, sampleRate, phase)

    type RandomWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(randomWave, startTime, amplitude, frequency, sampleRate, phase)
