# Riverbend Zoo (CSCI 1260 Project 1)

**Name:** Andrew Joyner
**Class:** CSCI 1260
**Language:** C# (.NET 10)
**World:** A zoo management system where animals are fed, weighed, and trained.

## Design questions

### 1. The two diamonds
The relationship between Zoo and Animal is aggregation (hollow diamond). 
Program creates every animal with new Lion(...), new Elephant(...), and new Parrot(...), then hands each one to Zoo.Add, and Zoo never calls new on an animal. 
If the zoo is removed, the animals survive, because Program still holds them and each animal keeps its own data and feeding history. 
The relationship between Animal and FeedingRecord is composition (filled diamond). 
Only Animal.Feed ever calls new FeedingRecord(...), the history list is private, and FeedingLines() returns a string and never the list itself, so when an animal is removed its feeding records go with it. 
The Zoo diamond would become filled if Add took raw values like an id, name, and feed amount and built the animal itself. 
The Animal diamond would become hollow if I added a public AddFeeding(FeedingRecord) method so outside code could build a record and hand it in.

### 2. The interface
ITrainable is implemented by Elephant and Parrot, which sit in different branches of the tree: 
Elephant is under the abstract Mammal, and Parrot hangs directly off Animal. 
Its members, TrickName and Perform(), could not go on Animal because every animal would then have to carry them, including Lion, which cannot be trained and would have to fake an answer or throw an error. 
They also could not go on Mammal, because Parrot is not a mammal and would miss out. 
An interface cuts across the tree in a way a base class can’t. 
It lets the two classes that can keep the promise sign it, so Zoo.TrainableCount() can ask is ITrainable and ShowTrick(ITrainable t) can accept an Elephant or a Parrot without knowing which.

### 3. Adding a new kind
I would add a Giraffe, a concrete class under Mammal that overrides Category(), DailyFoodCost(), and Describe(), with a field such as heightCm.
I would create one new file, Giraffe.cs. 
I would edit Program.cs to build a giraffe and add it to the zoo, and to teach ParseAnimal the new Giraffe line type, and I would optionally add a giraffe line to animals.txt to load one from the file. 
Zoo.cs does not change at all, because Zoo only knows about Animal: 
Add accepts any animal, and TotalMonthlyFoodCost() calls MonthlyFoodCost() on each one, so a giraffe’s own DailyFoodCost() runs automatically. 
Animal.cs and Mammal.cs also stay the same, since a giraffe signs no interface, and only the UML diagram would need a new box.

## Recording
I will put the link here once I record it
