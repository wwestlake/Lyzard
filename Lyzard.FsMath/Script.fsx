
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

#r @"../packages/NAudio.1.8.5/lib/net35/NAudio.dll"
#I "../packages/FSharp.Charting.2.1.0"
#load "FSharp.Charting.fsx"
#load "Generators.fs"
#load "SignalProcessing.fs"

open System
open Lyzard.FsMath.Generators
open Lyzard.SignalProcessing.Mixxers
open FSharp.Charting
open NAudio
open NAudio.Wave
open System.Linq
// Define your library scripting code here

let gen1 = new TriangleWaveGenerator(0.0f, 1.0f, 1000.0f, 44100.0f, 0.0f)
let gen2 = new SquareWaveGenerator(0.0f, 1.0f, 2000.0f, 44100.0f, 0.0f)
let gen3 = new SineWaveGenerator(0.0f, 1.0f, 4000.0f, 44100.0f, 0.0f)
let proc = new DSP()
let mix = proc.Mix( (proc.Mix(gen1.Generate(), gen2.Generate())), gen3.Generate());
let x = mix.Take(1000) |> Seq.toList
//
//let vals = sineWave 0.0 1.0 1000.0 44100.0 0.0 |> Seq.take(1000) |> Seq.toList

x |> Chart.Line
