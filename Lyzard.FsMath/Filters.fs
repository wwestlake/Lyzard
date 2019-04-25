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


