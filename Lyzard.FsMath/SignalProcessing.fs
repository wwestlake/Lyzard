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

    let rootMeanSquare n (a: float32 seq) : float32 =
        let sumSqr = a |> Seq.take(n) |> Seq.reduce (fun x y -> x + (y * y))
        let meanSqr = sumSqr / float32(n)
        let rms = Math.Sqrt(float(meanSqr))
        float32(rms)

        
    let fullRectify a =
        a |> Seq.map (fun x -> if x < 0.0f then -x else x)
       
    let halfRectify a =
        a |> Seq.map (fun x -> if x < 0.0f then 0.0f else x)


    let lowPassfilter (dt:float32) (rc:float32) (a:float32 seq) =
        let alpha = dt / (rc + dt)
        let y0 = alpha * ((Seq.take (1) a) |> Seq.last) 
        let rec inner prev list = 
            seq {
                let current = (Seq.take (1) list |> Seq.last)
                yield alpha * current + (1.0f - alpha) * prev
                yield! inner current a
            }
        inner y0 a

    //let lowPassfilter (dt:float32) (rc:float32) (a:float32 seq) =
    //    let alpha = dt / (rc + dt)
    //    let y0 = alpha * ((Seq.take (1) a) |> Seq.last) 
    //    a |> Seq.pairwise |> Seq.scan (fun elem prevOut -> ( alpha * (snd elem) + (1.0f - alpha) * (fst elem) ))



    type DSP() =
        member x.Mix(a,b) =
            mixxer a b
 
        member x.Clamp(min, max, a) =
            clamp min max a

        member x.Amplify(factorDb, a) =
            amplify factor a

