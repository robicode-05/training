module Program.webapi

open Microsoft.AspNetCore.Builder
open System
open Microsoft.AspNetCore.Http
open Apis
open DapperExtensions

open Dapper.FSharp.SQLite
open System.Data

type Product = {
    Name: string
    Price: int
}

Dapper.FSharp.SQLite.OptionTypes.register()

// let connectionString: IDbConnection = "Data Source=:memory:;Version=3;New=True;"
let connectionString: IDbConnection =  new SQLite.SQLiteConnection("Data Source=:memory:;Version=3;New=True;")

// let productTable = table<Product>
let productTable = table'<Product> "Products"


let init (conn:IDbConnection) =
    task {
        do! "DROP TABLE IF EXISTS Products" |> conn.ExecuteIgnore
        do!
            """
            CREATE TABLE Products (
                Name TEXT PRIMARY KEY,
                PRICE INTEGER
            )
            """
            |> conn.ExecuteIgnore
        return ()
    }


let postProductInDB = 
    Func<IResult>(fun () -> 
        insert {
            into productTable
            value { Name = "François"; Price = 12 }
        } |> connectionString.InsertAsync
        |> ignore
        Results.Ok()
    )

let builder = WebApplication.CreateBuilder()
let app = builder.Build()


app.MapGet("/", helloWorld) |> ignore
app.MapPost("/products", postProductInDB) |> ignore

app.Run()

type Program() = class end
