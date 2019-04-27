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
    
