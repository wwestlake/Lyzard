namespace Lyzard.FsMath

open System

/// Converts functions from C# form to F# form that can be passed as values
/// to F# functions.  Usage in C# is:
/// var func = Converters.convert2<double,double,double>( (x, y) => Math.Exp(x) );
module Converters =

    let convert1 (f:Func<'a>) =
        let inner =
            f.Invoke()
        inner

    let convert2 (f:Func<'a, 'b>) =
        let inner a =
            f.Invoke(a)
        inner

    let convert3 (f:Func<'a, 'b, 'c>) =
        let inner a b =
            f.Invoke(a,b)
        inner

    let convert4 (f:Func<'a, 'b, 'c, 'd>) =
        let inner a b c =
            f.Invoke(a,b,c)
        inner

    let convert5 (f:Func<'a, 'b, 'c, 'd, 'e>) =
        let inner a b c d =
            f.Invoke(a,b,c,d)
        inner

    let toTimeSeq (s : (float * float) seq) =
        s |> Seq.map (fun (t,x) -> t)

    let toValueSeq (s : (float * float) seq) =
        s |> Seq.map (fun (t,x) -> x)

    let toFloat32Seq s =
        s |> Seq.map (fun x -> float32(x))

    let toTimeSeqFloat32 s = s |> (toTimeSeq >> toFloat32Seq)

    let toValueSeqFloat32 s = s |> (toValueSeq >> toFloat32Seq)


