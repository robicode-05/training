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


[<Fact>]
let ``Should return 200 ok when send product`` () =
    let httpClient = create().CreateClient()
    task {
        let! (response: Http.HttpResponseMessage) = httpClient.PostAsync(
            "/products",
            JsonContent.Create({ Id= Guid.NewGuid().ToString(); Name = "François"; Price = 12})) 
        response.StatusCode.Should().Be(HttpStatusCode.OK, "Request should pass") |> ignore
    }

[<Fact>]
let ``Should add to storage when has new product`` () =
    let httpClient = create().CreateClient()

    task {
        do! init connectionString

        let newGuid = Guid.NewGuid().ToString()
        let personToInsert = { Id = newGuid; Name = "John"; Price = 55 }

        let! (response: Http.HttpResponseMessage) = httpClient.PostAsync(
            "/products",
            JsonContent.Create(personToInsert))

        response.StatusCode.Should().Be(HttpStatusCode.OK, "Request should pass") |> ignore
        let! fromDb =
            select {
                for p in productTable do
                where (p.Id = newGuid)
            } |> connectionString.SelectAsync<Product>

        (Seq.head fromDb).Should().Be(personToInsert, "we insert john") |> ignore
    } 