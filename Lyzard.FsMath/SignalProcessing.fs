namespace Lyzard.SignalProcessing

module Mixxers =

    let mixxer a b =
        Seq.zip a b |> Seq.map (fun (a,b) -> (a + b) / 3.0f)

    let clamp (min:float32) (max:float32) (a: float32 seq) =
        a |> Seq.map (fun x -> if x > max then max else if x < min then min else x )

    let amp (factor:float32) a =
        a |> Seq.map (fun x -> x * factor)

    

    type DSP() =
        member x.Mix(a,b) =
            mixxer a b
 
        member x.Clamp(min, max, a) =
            clamp min max a

        member x.Amplify(factor, a) =
            amp factor a

