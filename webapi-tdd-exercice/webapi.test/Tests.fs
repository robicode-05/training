module Tests

open System
open Xunit
open Microsoft.AspNetCore.Mvc.Testing
open System.Net.Http.Json
open FluentAssertions
open System.Net
open Program.webapi


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
        let! (response: Http.HttpResponseMessage) = httpClient.PostAsync(
            "/products",
            JsonContent.Create({ Name = "Souris"; Price = 55})) 

        let! productsFromDb =
            select {
                for p in productTable do
                selectAll
            } |> connectionString.SelectAsync<Product>

        productsFromDb.GetEnumerator().Current.Name.Should().Be("Souris", "Last input was Souris") |> ignore
    }