# Internship-Task

This is console application, which loads invetory of CPUs, Motherboards and Memorys and alow the user to enter their part numbers and generate the configurations.

## Table of contents
- [Technologies used](#technologies-used)
- [Usage/Examples](#usageexamples)
- [Note](#note)

## Technologies used
- C#
- .NET 7.0
- JSON

## Usage/Examples

### Search all posible combinations
Please enter part number(s): 12900K

There are 4 possible combinations:

Combination 1
CPU - IntelR CoreT i9-12900K Processor -LGA1700, DDR5

Motherboard - MSI MAG Z690 TORPEDO - LGA1700

Memory - Kingston 16GB KF560C36BBEK2-32 - DDR5

Price: 1061

Combination 2

. . .

### Search compositon
Please enter part number(s): 12900K, KS16GB, MSIZ690

CPU - IntelR CoreT i9-12900K Processor -LGA1700, DDR5

Memory - Kingston 16GB KF560C36BBEK2-32 - DDR5

Motherboard - MSI MAG Z690 TORPEDO - LGA1700

Price: 1061

. . .

## Note
If you want to load the json data from presented file change the path property in Store.cs.
