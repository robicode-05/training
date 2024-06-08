module FizzBuzz

let private isDivisibleBy number divider =
    number % divider = 0

let RunFizzBuzz (number: int) = 
    match number with
    | num when isDivisibleBy num 15 -> "FizzBuzz"
    | num when isDivisibleBy num 5 -> "Buzz"
    | num when isDivisibleBy num 3-> "Fizz"
    | _ -> (string number)



