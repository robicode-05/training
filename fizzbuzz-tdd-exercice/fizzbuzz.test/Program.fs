module Program 

open FizzBuzz

[<EntryPoint>]
let  main _ =
    printfn "%s" "FizzBuzz demo in F#"
    printfn "%s" (FizzBuzz.RunFizzBuzz 5)
    0
