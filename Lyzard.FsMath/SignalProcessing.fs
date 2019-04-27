namespace Lyzard.SignalProcessing
open System

module Mixxers =

    let decibelsToAttenuation (db:float32) : float32 =
        float32(Math.Pow(10.0, float(db/20.0f)))

    let mixxer a b =
        Seq.zip a b |> Seq.map (fun (a,b) -> (a + b) / 2.0f)

    let clamp (min:float32) (max:float32) (a: float32 seq) =
        a |> Seq.map (fun x -> if x > max then max else if x < min then min else x )

    let amp (factor:float32) a =
        a |> Seq.map (fun x -> x * factor)

    let amplify factorDb a =
        amp (decibelsToAttenuation factorDb) a

    let rootMeanSquare n (a: float32 seq) : float32 =
        let sumSqr = a |> Seq.take(n) |> Seq.reduce (fun x y -> x + (y * y))
        let meanSqr = sumSqr / float32(n)
        let rms = Math.Sqrt(float(meanSqr))
        float32(rms)

        
    let fullRectify a =
        a |> Seq.map (fun x -> if x < 0.0f then -x else x)
       
    let halfRectify a =
        a |> Seq.map (fun x -> if x < 0.0f then 0.0f else x)

    let offset dc a =
        a |> Seq.map (fun x -> x + dc)


    type DSP() =
        member x.Mix(a,b) =
            mixxer a b
 
        member x.Clamp(min, max, a) =
            clamp min max a

        member x.Amplify(factorDb, a) =
            amplify factorDb a

