module Apis

open Microsoft.AspNetCore.Http
open System

let helloWorld = 
    Func<IResult>(fun () -> Results.Ok("Hello World!"))

let postProduct = 
    Func<IResult>(fun () -> Results.Ok())