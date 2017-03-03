(*
    npm install --save-dev fable-compiler
    npm install --save fable-core lodash
    fable ch1.fsx --module commonjs
    node ch1.js
*)

#r "node_modules/fable-core/Fable.Core.dll"

printfn "### Sample 1 - First without lodash ###"
let collection = 
    [|
        "Lois"
        "Kathryn"
        "Craig"
        "Ryan"
    |]

collection |> Seq.iter (printfn "%s")

open Fable.Core.JsInterop

printfn "### Now we use lodash ###"
let lodash = importAll<obj> "lodash"

lodash?forEach(collection, printfn "%s")

let collection2 = 
    [|
        "Timothy"
        "Kelly"
        "Julia"
        "Leon"
    |]

printfn "### Sample 2 - First without lodash ###"
// Gets a little ugly...using mutable for this.
let mutable keepIterating = true
collection2
|> Seq.takeWhile (fun _ -> keepIterating)
|> Seq.iteri (fun i name -> 
    if name = "Kelly" then
        keepIterating <- false;
        printfn "%s Index: %i" name i
    )

printfn "### Now we use lodash ###"
lodash?forEach(collection2, fun name idx -> 
    if name = "Kelly" then
        printfn "%s Index: %i" name idx
        false
    else
        true
    )


type Named = {
    name : string
    }
let collection3 = 
    [|
        { name = "Moe"}
        { name = "Seymour"}
        { name = "Harold"}
        { name = "Willie"}
    |]

printfn "### Sample 3 - First without lodash ###"
collection3 |> Seq.sortBy (fun n -> n.name)
|> Seq.iter(printfn "%A")

printfn "### Sample 3 - Now we use lodash ###"
lodash?sortBy(collection3, "name") :?> seq<Named>
|> Seq.iter(printfn "%A")
