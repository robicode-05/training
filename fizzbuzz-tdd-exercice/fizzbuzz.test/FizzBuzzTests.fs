module FizzBuzzTests

open System
open Xunit
open FluentAssertions
open FizzBuzz


// Given an integer, return a string where:
//
// "FizzBuzz" if number is divisible by 3 and 5.
// "Fizz" if number is divisible by 3.
// "Buzz" if number is divisible by 5.
// number (as string) if none of the above conditions are true.


[<Theory>]
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(4)>]
[<InlineData(7)>]
let ``Given a number not divisable by 3 or 5 => returns number as string`` (number: int) =
    let results = FizzBuzz.RunFizzBuzz number
    results.Should().Be(number.ToString(), "not divisable by 3 or 5") |> ignore


[<Theory>]
[<InlineData(5)>]
[<InlineData(10)>]
[<InlineData(20)>]
[<InlineData(-25)>]
let ``Given a number is divisable by 5 => returns buzz`` (number: int) =
    let results = FizzBuzz.RunFizzBuzz number
    results.Should().Be("Buzz", "isDivisible by 5") |> ignore


[<Theory>]
[<InlineData(3)>]
[<InlineData(6)>]
[<InlineData(9)>]
[<InlineData(-12)>]
let ``Given a number is divisable by 3 => returns fizz`` (number: int) =
    let results = FizzBuzz.RunFizzBuzz number
    results.Should().Be("Fizz", "isDivisible by 3") |> ignore


[<Theory>]
[<InlineData(15)>]
[<InlineData(30)>]
[<InlineData(60)>]
[<InlineData(-60)>]
let ``Given a number is divisable by 3 and 5 => returns fizzbuzz`` (number: int) =
    let results = FizzBuzz.RunFizzBuzz number
    results.Should().Be("FizzBuzz", "isDivisible by 3") |> ignore
