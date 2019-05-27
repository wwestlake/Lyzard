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

