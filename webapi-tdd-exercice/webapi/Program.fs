module Program.webapi

open Microsoft.AspNetCore.Builder
open System
open Microsoft.AspNetCore.Http
open Apis


let builder = WebApplication.CreateBuilder()
let app = builder.Build()

app.MapGet("/", helloWorld) |> ignore
app.MapPost("/products", postProduct) |> ignore

app.Run()

type Program() = class end
