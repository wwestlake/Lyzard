
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

#r @"../packages/NAudio.1.8.5/lib/net35/NAudio.dll"
#I "../packages/FSharp.Charting.2.1.0"
#load "FSharp.Charting.fsx"
#load "Generators.fs"
#load "SignalProcessing.fs"
#load "Filters.fs"

open System
open Lyzard.FsMath.Generators
open Lyzard.SignalProcessing.Mixxers
open Lyzard.FsMath.Filters
open FSharp.Charting
open NAudio
open NAudio.Wave
open System.Linq
// Define your library scripting code here

//let gen1 = new TriangleWaveGenerator(0.0f, 1.0f, 1000.0f, 44100.0f, 0.0f)
//let gen2 = new SquareWaveGenerator(0.0f, 1.0f, 2000.0f, 44100.0f, 0.0f)
//let gen3 = new SineWaveGenerator(0.0f, 1.0f, 4000.0f, 44100.0f, 0.0f)
//let proc = new DSP()
//let mix = proc.Mix( (proc.Mix(gen1.Generate(), gen2.Generate())), gen3.Generate());
//let x = mix.Take(1000) |> Seq.toList
//
//let vals = sineWave 0.0 1.0 1000.0 44100.0 0.0 |> Seq.take(1000) |> Seq.toList

//x |> Chart.Line


let t = seq { for i in 1.0 .. 100.0 do if i = 3.0 then yield 1.0 else yield 0.0 }

  
let filter n f coefA coefB data =
    let inner list x =
        match list with
        | [] -> (0.0::list, 0.0)
        | l::tail ->
               printfn "%d" (List.length (l::tail))
               let sublist = if List.length (l::tail) >= n then tail else l::tail
               let result = f sublist coefA coefB x
               (result::sublist, result)
    data |> Seq.scan (fun (list, xprev) x -> (inner list x) ) ([],0.0)
         |> Seq.map (fun (l,x) -> x)

let iir list coefA coefB x = 
    (List.sum list) / float(List.length list) * coefA + coefB * x

let testiir coefA coefB data = filter 3 iir coefA coefB data

testiir 2.0 3.0 t |> Seq.iter (fun x -> printfn "%f" x)


/// improved filter multiple coefs and multiple feed back 

let data = seq { for x in 1.0 .. 1000.0 -> 
                        if x >= 5.0 then 1.0 else 0.0 
                        
                    }
let coefs =  [ 0.5; 0.4; 0.3 ]

let filter (coefs:float list) (data: float seq) =
    let n = List.length coefs
    let rec inner c p x =
        match c,p with
        | [], []  
        | _, [] 
        | [], _ -> x
        | ch::ct, ph::pt ->
            //printfn "%f * %f + %f = %f" ch ph x (ch * ph + x)
            inner ct pt (ch * (ph + x))

    data |> Seq.scan (fun (prev, y) x -> 
                        match prev with
                        | [] -> ([y], x)
                        | list when (List.length list) < n -> (prev @ [y], inner coefs (prev @ [y]) x)
                        | h::t -> (t @ [y], inner coefs (t @ [y]) x)
                     ) ([],0.0) |> Seq.map (fun (l,x) -> x)

filter coefs data |> Seq.iter (fun x -> printfn "%A" x)

(*
a0 0.0004218886 0.0004218886
a1 -0.001687554 -0.00161024
a2 0.0025313316 0.00229933
a3 -0.001687554 -0.0014555
a4 0.0004218886 0.000344561
b1 1 1
b2 3.177811 3.15992
b3 3.8546544 3.80985
b4 2.10730622 2.069939
b5 0.43700264 0.426703
*)


// more improvement
let data2 = seq { for x in 1.0 .. 20.0 -> 
                        if x >= 2.0 then 1.0 else 0.0 
                        //x
                    }
let coefsA =  [ 0.0004218886; -0.001687554; 0.0025313316; -0.001687554; 0.0004218886]
let coefsB =  [ 1.0; 3.177811; 3.8546544; 2.10730622; 0.43700264]

let filter2 (coefsA:float list) (coefsB:float list) (data: float seq) =
    let n = List.length coefsA
    let rec inner cIn cOut pin pout x =
        match cIn,cOut, pin, pout with
        | [], [], [], []  
        | [], _, _, _
        | _, [], _, _  
        | _, _, [], _
        | _, _, _, [] -> x
        | A::cint, B::coutt, pinh::pint, pouth::poutt ->
            //printfn "%f * %f + %f = %f" ch ph x (ch * ph + x)
            inner cint coutt pint poutt ((A * pouth + B * pinh ) * float(n))

    data |> Seq.scan (fun (prevIn, prevOut, y) x -> 
                        match prevIn, prevOut with
                        | [],[] 
                        | _, []
                        | [], _ -> ([x],[y], x)
                        | pin, pout when (List.length pin) < n -> 
                                prevIn @ [x],  prevOut @ [y], inner coefsA coefsB (prevIn @ [x]) (prevOut @ [y]) x 
                        | pinh::pint, pouth::poutt -> pint @ [x], poutt @ [y], inner coefsA coefsB (pint @ [x]) (poutt @ [y]) x
                     ) ([],[],0.0) |> Seq.map (fun (l1,l2,x) -> x)

NpoleIIRFilter coefsA coefsB data2 
    //|> Seq.iter (fun x -> printfn "%A" x) 
    |> Chart.Spline


