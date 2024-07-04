module Program.webapi

open Microsoft.AspNetCore.Builder
open System
open Microsoft.AspNetCore.Http
open Apis
open DapperExtensions
open Dapper.FSharp.SQLite
open System.Data

type Product = {
    Id: string
    Name: string
    Price: int
}



// let connectionString: IDbConnection = "Data Source=:memory:;Version=3;New=True;"
//let connectionString: IDbConnection =  new SQLite.SQLiteConnection("Data Source=:memory:;Version=3;New=True;")
let connectionString: IDbConnection =  new SQLite.SQLiteConnection("Data Source=IntegrationTests.sqlite")

// let productTable = table<Product>
let productTable = table'<Product> "Products"


let init (conn:IDbConnection) =

    task {
        conn.Open()
        do! "DROP TABLE IF EXISTS Products" |> conn.ExecuteIgnore
        do!
            """
            CREATE TABLE [Products] (
                [Id] [TEXT] NOT NULL PRIMARY KEY,
                [Name] [TEXT] NOT NULL,
                [Price] [INTEGER] NOT NULL
            )
            """
            |> conn.ExecuteIgnore
        return ()
        Dapper.FSharp.SQLite.OptionTypes.register()
    }
   



let postProductInDB = 
    Func<Product, IResult>(fun (product) -> 
        task {           
            insert {
                into productTable
                value product
            }
            |> connectionString.InsertAsync
            |> ignore
        } |> ignore
        Results.Ok()
    )

let builder = WebApplication.CreateBuilder()
let app = builder.Build()


app.MapGet("/", helloWorld) |> ignore
app.MapPost("/products", postProductInDB) |> ignore

app.Run()

type Program() = class end
