# Lottery Coding Exercise

This repository is a resource for a coding exercise. The exercise has 2 parts. The first part is a simple exercise to get you started. The second part is more complex with greater scope for creativity.

A lottery ticket is a set of 6 numbers from 1 to 59 inclusive without duplicates. A lottery draw is a single set of numbers adhering to the same criteria as a lottery ticket.

## Part 1 - Counting Winning Tickets

Given a set lottery tickets and a lottery draw determine the number of tickets matching 3, 4, 5 or 6 numbers. Count the maximum number of matches with the lottery draw only. For example, if a ticket matches 3 numbers and 4 numbers then only count it as matching 4 numbers.

## Part 2 - Maximise Winning Tickets

Given a set of lottery tickets determine the lottery draw that would maximise the number of winning tickets matching 3 numbers.

## Notes

An example file named `Tickets100.csv` containing 100 tickets is provided. Given a lottery draw of `4,10,42,53,56,58` the number of winning tickets is 2 for 3 numbers, 1 for 4 numbers, 1 for 5 numbers and 1 for 6 numbers.

### Ticket File Generator
A ticket file generator is provided to generate large datasets. For example, to generate 1,000,000 tickets run the following command

```powershell
dotnet .\InputGenerator.dll number 1000000
```

For further details on how to use the input generator run the following command

```powershell
dotnet .\InputGenerator.dll --help
```

### Example Implementation

An example implementation is provided. The intent of the example implementation is to be easy to read and it has not been optimised for performance.

The following powershell commands run and time the example implementation respectively.

```powershell
dotnet .\Lottery.dll Tickets100.csv mode count draw 4,10,42,53,56,58
Measure-Command { dotnet .\Lottery.dll Tickets100.csv mode count draw 4,10,42,53,56,58 }
```

The example implementation takes 97ms to run on my machine using the command above. It takes 1371ms to run on a tickets file containing 1 million tickets. Run the same on your machine to get a baseline for performance.