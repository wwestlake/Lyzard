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

module Filters =


    let filter f init coefA coefB data =
        data |> Seq.scan (fun xprev x -> f coefA coefB xprev x ) init

    let filterWithTime f init coefA coefB data =
        data |> Seq.scan (fun (_, xprev) (t,x) -> (t, f coefA coefB xprev x)) init

    let IIR coefA coefB xprev (x:float) =
        coefA * xprev + coefB * x

    let IIRFilter init  = filter IIR init
    let IIRFilterWithTime init = filterWithTime IIR init

    let NpoleIIRFilter (coefsA:float list) (coefsB:float list) (data: float seq) =
        let n = List.length coefsA
        let rec inner cIn cOut pin pout x =
            match cIn,cOut, pin, pout with
            | [], [], [], []  
            | [], _, _, _
            | _, [], _, _  
            | _, _, [], _
            | _, _, _, [] -> x
            | A::cint, B::coutt, pinh::pint, pouth::poutt ->
                inner cint coutt pint poutt ((A * pouth + B * pinh ))
    
        data |> Seq.scan (fun (prevIn, prevOut, y) x -> 
                            match prevIn, prevOut with
                            | [],[] 
                            | _, []
                            | [], _ -> ([x],[y], x)
                            | pin, _ when (List.length pin) < n -> 
                                    prevIn @ [x],  prevOut @ [y], inner coefsA coefsB (prevIn @ [x]) (prevOut @ [y]) x 
                            | _::pint, _::poutt -> pint @ [x], poutt @ [y], inner coefsA coefsB (pint @ [x]) (poutt @ [y]) x
                         ) ([],[],0.0) |> Seq.map (fun (l1,l2,x) -> x)
    
