# Lottery Coding Exercise

This repository is a resource for a coding exercise. The exercise has 2 parts. The first part is a simple exercise to get you started. The second part is more complex with greater scope for creativity.

A lottery ticket is a set of 6 numbers from 1 to 59 inclusive without duplicates. A lottery draw is a single set of numbers adhering to the same criteria as a lottery ticket.

## Part 1 - Counting Winning Tickets

Given a set lottery tickets and a lottery draw determine the number of tickets matching 3, 4, 5 or 6 numbers. Count the maximum number of matches with the lottery draw only. For example, if a ticket matches 3 numbers and 4 numbers then only count it as matching 4 numbers.

## Part 2 - Maximise Winning Tickets

Given a set of lottery tickets determine the lottery draw that would maximise the number of winning tickets matching 3 numbers.

## Notes

An example file named `Tickets100.csv` containing 100 tickets is provided. Given a lottery draw of `2,21,25,45,52,56` the number of winning tickets is 0 for 3 numbers, 0 for 4 numbers, 0 for 5 numbers and 1 for 6 numbers.

An input generator is provided to generate large datasets. For example, to generate 10,000,000 tickets run the following command

```powershell
dotnet .\InputGenerator.dll number 10000000
```

For further details on how to use the input generator run the following command

```powershell
dotnet .\InputGenerator.dll --help
```