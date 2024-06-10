module Tests

open System
open Xunit
open Microsoft.AspNetCore.Mvc.Testing
open System.Net.Http.Json
open FluentAssertions
open System.Net
open Program.webapi

let create () = (new WebApplicationFactory<Program>()).Server

type Person = {
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

// [<Fact>]
// let ``Should add to storage when has new product`` () =
//     let httpClient = create().CreateClient()