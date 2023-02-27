# Lottery Coding Exercise

This project is a resource for a coding exercise.

## Part 1 - Counting Winning Tickets

Given a set lottery tickets and a set of drawn numbers determine the number of tickets matching 3, 4, 5 or 6 numbers in the draw. Count the maximum number of matches only; if a ticket matches 3 numbers and 4 numbers then only count it as matching 4 numbers.

## Part 2 - Maximise Winning Tickets

Given a set of lottery tickets determine the lottery draw that would maximise the number of winning tickets matching 3 numbers.

## Notes

This exercise is intended for participants with a wide range of abilities and interests. A sample implementation is provided both for guidance (if required) and to allow participants to skip aspects they're not interested in.

The exercise has 2 parts. The first part is a simple exercise to get you started. The second part is more complex with greater scope for creativity.

A lottery ticket is a set of 6 numbers from 1 to 59 inclusive without duplicates. A lottery draw is a single set of numbers adhering to the same criteria as a lottery ticket.

An example file named `Tickets1K.csv` containing 1000 tickets is provided. Given a lottery draw of `5,10,20,34,42,57` there are 11 tickets matching 3 numbers, 1 matching 4 numbers, 1 matching 5 numbers and 1 matching 6 numbers.

```powershell
> dotnet .\Lottery.dll .\Tickets1K.csv mode count draw "5,10,20,34,42,57"
Matches: 3:11, 4:1, 5:1, 6:1
```

In `Tickets1K.csv` there are 3 three number combinations matching 6 or more tickets

```powershell
> dotnet .\Lottery.dll .\Tickets1K.csv mode maximise min 6
Most common three number combinations (3)
[11,34,45] (7)
[7,34,54] (6)
[24,37,51] (6)
```

### Performance

The performance of the sample implementation on my system has been measured. Measure the same execution time on your system and use it as a baseline for evaluating the performance of your solution.

#### File `Tickets1K.csv`

```powershell
> Measure-Command { dotnet .\Lottery.dll .\Tickets1K.csv mode count draw "5,10,20,34,42,57" }
TotalMilliseconds : 102

> Measure-Command { dotnet .\Lottery.dll .\Tickets1K.csv mode maximise min 6 }
TotalMilliseconds : 135
```

#### File `Tickets1M.csv`

File containing 1,000,000 tickets (not provided). Generate one using the provided tool.

```powershell
> Measure-Command { dotnet .\Lottery.dll .\Tickets1M.csv mode count draw "5,10,20,34,42,57" }
TotalMilliseconds : 1876

> Measure-Command { dotnet .\Lottery.dll .\Tickets1M.csv mode maximise min 700 }
TotalMilliseconds : 12925
```

### Ticket File Generator
A ticket file generator is provided to generate large datasets. For example, to generate 1,000,000 tickets run the following command

```powershell
> dotnet .\InputGenerator.dll number 1000000
```