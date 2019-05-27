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

module Solvers =


    /// Fourth order Runge-Kutta ODE Solver 
    let RungeKutta4 x y h n f =
        let half = 0.5
        let sixth = 1.0/6.0
        let two = 2.0
        let rungekutta4' x y h f =
            let k1 = h * f x y
            let k2 = h * f (x + h * half) (y + k1 * half)
            let k3 = h * f (x + h * half) (y + k2 * half)
            let k4 = h * f (x + h) (y + k3)
            y + sixth * (k1 + two * k2 + two * k3 + k4)

        let rec inner x y h n f incr results =
            let y' = rungekutta4' x y h f
            if (x >= n) then (incr, results)
            else inner (x+h) y' h n f (x+h :: incr) (y' :: results)
        let (incr', result') = inner x y h n f [] [] 
        List.zip incr' result' |> List.rev


    /// Euler's first order solver for ODE's
    let Euler f a b n y0 =
        let h = (b - a) / float n
        let rec euler' f x y0 h =
            match x with
            | x0::x1::xt -> let y1 = y0 + (f x0 y0) * h
                            let yx = y0 + ((f x0 y0) + (f x1 y1)) / 2.0 * h
                            printfn "%.06f %.06f %.06f" x1 y1 yx
                            euler' f (x1::xt) yx h
            | _ -> y0
        euler' f [a..h..b] y0 h

   

