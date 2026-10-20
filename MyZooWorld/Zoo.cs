using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld;

// The manager. Holds animals that Program built and passed to Add (aggregation).
// Never calls new on an Animal.
public class Zoo
{
    private string name;
    private List<Animal> animals;

    public string Name { get { return name; } }
    public int Count { get { return animals.Count; } }

    public Zoo(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Zoo name cannot be blank.");

        this.name = name;
        animals = new List<Animal>();
    }

    public bool Add(Animal animal)
    {
        if (animal == null)
            return false;
        if (Find(animal.Id) != null)
            return false;

        animals.Add(animal);
        return true;
    }

    public Animal Find(string id)
    {
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i].Id == id)
                return animals[i];
        }
        return null;
    }

    // One loop, no type tests. Each MonthlyFoodCost() call lands in a
    // different DailyFoodCost() override.
    public decimal TotalMonthlyFoodCost()
    {
        decimal total = 0m;
        for (int i = 0; i < animals.Count; i++)
        {
            total += animals[i].MonthlyFoodCost();
        }
        return total;
    }

    // The one place a type test is right: the question is
    // "did this animal sign the contract?"
    public int TrainableCount()
    {
        int count = 0;
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i] is ITrainable)
                count++;
        }
        return count;
    }

    public string Report()
    {
        string text = String.Format("{0}: {1} animals", Name, Count);
        for (int i = 0; i < animals.Count; i++)
        {
            text += "\n" + String.Format("  {0} - ${1:N2}/month",
                animals[i].Describe(), animals[i].MonthlyFoodCost());
        }
        text += "\n" + String.Format("  Total monthly food cost: ${0:N2}",
            TotalMonthlyFoodCost());
        return text;
    }
}
