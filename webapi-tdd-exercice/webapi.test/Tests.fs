module Tests

open System
open Xunit
open Microsoft.AspNetCore.Mvc.Testing
open System.Net.Http.Json
open FluentAssertions
open System.Net
open Program.webapi
open System.Linq

open Dapper.FSharp.SQLite
open System.Collections

let create () = (new WebApplicationFactory<Program>()).Server

type Product = {
    Name: string
    Price: int
}

[<Fact>]
let ``Should return 200 ok when send product`` () =
    let httpClient = create().CreateClient()
    task {
        let! (response: Http.HttpResponseMessage) = httpClient.PostAsync(
            "/products",
            JsonContent.Create({ Name = "François"; Price = 12})) 
        response.StatusCode.Should().Be(HttpStatusCode.OK, "Request should pass") |> ignore
    }

[<Fact>]
let ``Should add to storage when has new product`` () =
    let httpClient = create().CreateClient()

    task {
        do! init connectionString

        let! (response: Http.HttpResponseMessage) = httpClient.PostAsync(
            "/products",
            JsonContent.Create({ Name = "Souris"; Price = 55}))

        response.StatusCode.Should().Be(HttpStatusCode.OK, "Request should pass") |> ignore

        let! fromDb =
            select {
                for p in productTable do
                where (p.Name = "Souris")
            } |> connectionString.SelectAsync<Product>

        fromDb.ElementAt(0).Name.Should().Be("Souris", "we insert souris") |> ignore
    } 