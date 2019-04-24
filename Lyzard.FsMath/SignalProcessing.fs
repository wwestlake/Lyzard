namespace Lyzard.SignalProcessing

module Mixxers =

    let decibelsToAttenuation db =
        Math.Pow(10., db/20.)

    let mixxer a b =
        Seq.zip a b |> Seq.map (fun (a,b) -> (a + b) / 2.0f)

    let clamp (min:float32) (max:float32) (a: float32 seq) =
        a |> Seq.map (fun x -> if x > max then max else if x < min then min else x )

    let amp (factor:float32) a =
        a |> Seq.map (fun x -> x * factor)

    let amplifier factorDb a =
        amp (decibelsToAttenuation factorDb) a

    type DSP() =
        member x.Mix(a,b) =
            mixxer a b
 
        member x.Clamp(min, max, a) =
            clamp min max a

        member x.Amplify(factorDb, a) =
            amplify factor a

