namespace MyZooWorld;

// Entry point. Builds every animal with new (so the Zoo only aggregates them)
// and runs the five output sections.
internal class Program
{
    // Takes the interface type, so it accepts an Elephant or a Parrot.
    static void ShowTrick(ITrainable trainer)
    {
        Console.WriteLine("  " + trainer.Perform());
    }

    static void Main(string[] args)
    {
        // Animal bad = new Animal("X", "Nope", 1.0); // CS0144: cannot create an instance of the abstract type


        Console.WriteLine("=== 1. Classes, UML, and Relationships ===");
        Zoo zoo = new Zoo("Riverbend Zoo");
        Console.WriteLine("{0}: {1} animals", zoo.Name, zoo.Count);

        Lion nala = new Lion("LN01", "Nala", 6.0, 110, 8);
        Elephant tembo = new Elephant("EL01", "Tembo", 150.0, 0, 180.0);
        Parrot kiwi = new Parrot("PR01", "Kiwi", 0.2, 90.0);
        Parrot rio = new Parrot("PR02", "Rio", 0.3, 85.0);
        Lion duplicate = new Lion("LN01", "Simba", 6.5, 0, 8);

        zoo.Add(nala);
        zoo.Add(tembo);
        zoo.Add(kiwi);
        zoo.Add(rio);

        if (!zoo.Add(duplicate))
        {
            Console.WriteLine("  REJECTED: duplicate id LN01");
        }
        Console.WriteLine("Animals in zoo: {0}", zoo.Count);

        nala.Feed(5.5);
        nala.Feed(4.0);
        if (!nala.Feed(-1))
        {
            Console.WriteLine("  REJECTED: feed of -1 kg for LN01");
        }
        Console.WriteLine("Feedings recorded for Nala: {0}", nala.FeedCount);
        Console.WriteLine(nala.FeedingLines());
        Console.WriteLine();


        Console.WriteLine("=== 2. Inheritance and Abstraction ===");
        Console.WriteLine(nala);
        Console.WriteLine(tembo);
        Console.WriteLine(kiwi);
        Console.WriteLine("Nala monthly food cost: ${0:N2}", nala.MonthlyFoodCost());
        Console.WriteLine();


        Console.WriteLine("=== 3. Polymorphism and Interfaces ===");
        List<Animal> allAnimals = new List<Animal>();
        allAnimals.Add(nala);
        allAnimals.Add(tembo);
        allAnimals.Add(kiwi);
        allAnimals.Add(rio);

        foreach (Animal animal in allAnimals)
        {
            Console.WriteLine("  {0} ({1}): ${2:N2}/month",
                animal.Name, animal.Category(), animal.MonthlyFoodCost());
        }
        Console.WriteLine("Total monthly food cost: ${0:N2}", zoo.TotalMonthlyFoodCost());

        ShowTrick(tembo);
        ShowTrick(kiwi);
        Console.WriteLine("Trainable animals: {0}", zoo.TrainableCount());
        Console.WriteLine();


        Console.WriteLine("=== 4. Built-in Contracts and Collections ===");
        List<HealthCheck> checks = new List<HealthCheck>();
        checks.Add(new HealthCheck("EL01", new DateTime(2026, 3, 1), 5200.0));
        checks.Add(new HealthCheck("LN01", new DateTime(2026, 2, 10), 190.5));
        checks.Add(new HealthCheck("PR01", new DateTime(2026, 1, 15), 0.9));
        checks.Add(new HealthCheck("LN01", new DateTime(2026, 2, 10), 192.0)); // duplicate key
        checks.Add(new HealthCheck("EL01", new DateTime(2026, 1, 5), 5100.0));
        checks.Add(new HealthCheck("LN01", new DateTime(2026, 1, 20), 188.0));

        HashSet<HealthCheck> unique = new HashSet<HealthCheck>(checks);
        Console.WriteLine("Records loaded: {0}", checks.Count);
        Console.WriteLine("Unique after HashSet: {0}", unique.Count);

        List<HealthCheck> sorted = new List<HealthCheck>(unique);

        sorted.Sort();
        Console.WriteLine("Natural order (animal, then date):");
        foreach (HealthCheck check in sorted)
            Console.WriteLine("  " + check);

        sorted.Sort(new HealthCheckByWeight());
        Console.WriteLine("By weight:");
        foreach (HealthCheck check in sorted)
            Console.WriteLine("  " + check);

        sorted.Sort(new HealthCheckByDate());
        Console.WriteLine("By date:");
        foreach (HealthCheck check in sorted)
            Console.WriteLine("  " + check);
        Console.WriteLine();

        Console.WriteLine("=== 5. Exceptions and Files ===");
        Console.WriteLine("Loading animals.txt...");
        Zoo loadedZoo = new Zoo("Loaded Zoo");
        string dataPath = Path.Combine(AppContext.BaseDirectory, "animals.txt");
        string[] lines = File.ReadAllLines(dataPath);

        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                string[] fields = lines[i].Split(',');
                if (fields.Length < 5)
                    throw new FormatException("not enough fields");

                string kind = fields[0].Trim();
                string id = fields[1].Trim();
                string name = fields[2].Trim();
                double feed = double.Parse(fields[3]);
                Animal parsed;

                if (kind == "Lion" && fields.Length == 6)
                    parsed = new Lion(id, name, feed, int.Parse(fields[4]), int.Parse(fields[5]));
                else if (kind == "Elephant" && fields.Length == 6)
                    parsed = new Elephant(id, name, feed, int.Parse(fields[4]), double.Parse(fields[5]));
                else if (kind == "Parrot" && fields.Length == 5)
                    parsed = new Parrot(id, name, feed, double.Parse(fields[4]));
                else
                    throw new FormatException("unknown animal type or wrong field count: " + kind);

                loadedZoo.Add(parsed);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("  REJECTED line {0}: {1}", i + 1, ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  REJECTED line {0}: {1}", i + 1, ex.Message);
            }
        }
        Console.WriteLine("Animals loaded: {0}", loadedZoo.Count);

        Console.WriteLine("Constructor check:");
        try
        {
            Lion nameless = new Lion("", "Nameless", 5.0, 110, 3);
            Console.WriteLine(nameless);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("  Caught: " + ex.Message);
        }

        Console.WriteLine("Using block check:");
        string logPath = Path.Combine(AppContext.BaseDirectory, "run.log");
        RunLog log = new RunLog(logPath);
        try
        {
            using (log)
            {
                log.Write("Zoo opened");
                log.Write("Loaded " + loadedZoo.Count + " animals");
                log.Write("About to fail on purpose");
                throw new InvalidOperationException("Deliberate failure inside using block");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("  Caught: " + ex.Message);
        }
        log.Dispose();
        Console.WriteLine("  Second Dispose call was safe.");

        Console.WriteLine("Log file contents:");
        Console.Write(File.ReadAllText(logPath));
    }
}