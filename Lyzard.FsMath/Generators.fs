namespace Lyzard.FsMath

open System

module Generators =

    let PI = float32(Math.PI)

    let generatorWithTime f (start:float32) (amplitude:float32) (frequency:float32) (sampleRate:float32) (phase:float32)  : (float32 * float32) seq =
        let deltaTime = 1.0f / sampleRate
        let rec inner time : (float32 * float32) seq = seq {
            yield (time, f time amplitude frequency phase)
            yield! inner (time + deltaTime)
        }
        inner start


    let generator f (start:float32) (amplitude:float32) (frequency:float32) (sampleRate:float32) (phase:float32) : float32 seq =
        generatorWithTime f start amplitude frequency sampleRate phase
        |> Seq.map (fun x -> snd x)
    

    let sineFunc (time:float32) (amplitude:float32) (frequency:float32) (phase:float32) : float32 =
        let omega = (2.f * PI * frequency)
        float32(Math.Sin(float(omega * time + phase)) * float(amplitude))

    let squareFunc (time:float32) (amplitude:float32) (frequency:float32) (phase:float32) =
        let value = sineFunc time amplitude frequency phase
        if value >= 0.0f then amplitude else -amplitude

    let triangleFunc (time:float32) (amplitude:float32) (frequency:float32) (phase:float32) =
        let p = phase * 1.0f
        let omega = 2.0f * PI * frequency
        let deltaTime = 1.0f / frequency + (phase / omega)
        let adjustedTime = time + deltaTime
        let cycle = int (frequency * adjustedTime * 4.0f)
        let tao = adjustedTime - float32(cycle) / 4.0f / frequency
        let cyclePosition = int (frequency * adjustedTime * 4.0f) % 4
        let m = 4.0f * amplitude * frequency
        match cyclePosition with
        | 0 -> tao * m
        | 1 -> amplitude - tao * m
        | 2 -> -tao * m
        | _ -> -amplitude + tao * m

    let sineWave = generator sineFunc
    let sineWaveWithTime = generatorWithTime sineFunc
    let squareWave = generator squareFunc
    let squareWaveWithTime = generatorWithTime squareFunc
    let triangleWave = generator triangleFunc
    let triangleWaveWithTime = generatorWithTime triangleFunc
    

    type FunctionGenerator(func, startTime, amplitude, frequency, sampleRate, phase) =
        member private x.func = func
        member private x.startTime = startTime
        member private x.amplitude = amplitude
        member private x.frequency = frequency
        member private x.sampleRate = sampleRate
        member private x.phase = phase
        member x.Generate() =
            x.func x.startTime x.amplitude x.frequency x.sampleRate x.phase

    type SineWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(sineWave, startTime, amplitude, frequency, sampleRate, phase)

    type SquareWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(squareWave, startTime, amplitude, frequency, sampleRate, phase)

    type TriangleWaveGenerator(startTime, amplitude, frequency, sampleRate, phase) =
        inherit FunctionGenerator(triangleWave, startTime, amplitude, frequency, sampleRate, phase)


