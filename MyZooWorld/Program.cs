namespace MyZooWorld;

// Driver. Builds every animal with new (so the Zoo only aggregates them)
// and runs the five output sections.
public class Program
{
    static void ShowTrick(ITrainable t)
    {
        Console.WriteLine("  " + t.Perform());
    }

    static void PrintChecks(string title, List<HealthCheck> list)
    {
        Console.WriteLine(title);
        for (int i = 0; i < list.Count; i++)
            Console.WriteLine("  " + list[i]);
    }

    static Animal ParseAnimal(string line)
    {
        string[] p = line.Split(',');
        if (p.Length < 5)
            throw new FormatException("not enough fields");

        string kind = p[0].Trim();
        string id = p[1].Trim();
        string name = p[2].Trim();
        double feed = double.Parse(p[3]);

        if (kind == "Lion" && p.Length == 6)
            return new Lion(id, name, feed, int.Parse(p[4]), int.Parse(p[5]));
        if (kind == "Elephant" && p.Length == 6)
            return new Elephant(id, name, feed, int.Parse(p[4]), double.Parse(p[5]));
        if (kind == "Parrot" && p.Length == 5)
            return new Parrot(id, name, feed, double.Parse(p[4]));

        throw new FormatException("unknown animal type or wrong field count: " + kind);
    }

    static Zoo LoadZoo(string path)
    {
        Zoo loaded = new Zoo("Loaded Zoo");
        string[] lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                loaded.Add(ParseAnimal(lines[i]));
            }
            catch (FormatException ex)
            {
                Console.WriteLine(String.Format("  REJECTED line {0}: {1}", i + 1, ex.Message));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(String.Format("  REJECTED line {0}: {1}", i + 1, ex.Message));
            }
        }
        return loaded;
    }

    public static void Main(string[] args)
    {
        // Animal bad = new Animal("X", "Nope", 1.0); // CS0144: cannot create an instance of the abstract type

        // ---------- 1 ----------
        Console.WriteLine("=== 1. Classes, UML, and Relationships ===");
        Zoo zoo = new Zoo("Riverbend Zoo");
        Console.WriteLine(zoo.Name + ": " + zoo.Count + " animals");

        Lion nala = new Lion("LN01", "Nala", 6.0, 110, 8);
        Elephant tyla = new Elephant("EL01", "Tyla", 150.0, 650, 180.0);
        Parrot kiwi = new Parrot("PR01", "Kiwi", 0.2, 90.0);
        Parrot rio = new Parrot("PR02", "Rio", 0.3, 85.0);
        zoo.Add(nala);
        zoo.Add(tyla);
        zoo.Add(kiwi);
        zoo.Add(rio);

        if (!zoo.Add(new Lion("LN01", "Simba", 6.5, 0, 8)))
            Console.WriteLine("  REJECTED: duplicate id LN01");
        Console.WriteLine("Animals in zoo: " + zoo.Count);

        nala.Feed(5.5);
        nala.Feed(4.0);
        if (!nala.Feed(-1))
            Console.WriteLine("  REJECTED: feed of -1 kg for LN01");
        Console.WriteLine("Feedings recorded for Nala: " + nala.FeedCount);
        Console.WriteLine(nala.FeedingLines());
        Console.WriteLine();

        // ---------- 2 ----------
        Console.WriteLine("=== 2. Inheritance and Abstraction ===");
        Console.WriteLine(nala);
        Console.WriteLine(tyla);
        Console.WriteLine(kiwi);
        Console.WriteLine(String.Format("Nala monthly food cost: ${0:N2}", nala.MonthlyFoodCost()));
        Console.WriteLine();

        // ---------- 3 ----------
        Console.WriteLine("=== 3. Polymorphism and Interfaces ===");
        List<Animal> all = new List<Animal>();
        all.Add(nala);
        all.Add(tyla);
        all.Add(kiwi);
        all.Add(rio);
        foreach (Animal a in all)
        {
            Console.WriteLine(String.Format("  {0} ({1}): ${2:N2}/month",
                a.Name, a.Category(), a.MonthlyFoodCost()));
        }
        Console.WriteLine(String.Format("Total monthly food cost: ${0:N2}", zoo.TotalMonthlyFoodCost()));
        ShowTrick(tyla);
        ShowTrick(kiwi);
        Console.WriteLine("Trainable animals: " + zoo.TrainableCount());
        Console.WriteLine();

        // ---------- 4 ----------
        Console.WriteLine("=== 4. Built-in Contracts and Collections ===");
        List<HealthCheck> checks = new List<HealthCheck>();
        checks.Add(new HealthCheck("EL01", new DateTime(2026, 3, 1), 5200.0));
        checks.Add(new HealthCheck("LN01", new DateTime(2026, 2, 10), 190.5));
        checks.Add(new HealthCheck("PR01", new DateTime(2026, 1, 15), 0.9));
        checks.Add(new HealthCheck("LN01", new DateTime(2026, 2, 10), 192.0)); // duplicate key
        checks.Add(new HealthCheck("EL01", new DateTime(2026, 1, 5), 5100.0));
        checks.Add(new HealthCheck("LN01", new DateTime(2026, 1, 20), 188.0));

        HashSet<HealthCheck> unique = new HashSet<HealthCheck>(checks);
        Console.WriteLine("Records loaded: " + checks.Count);
        Console.WriteLine("Unique after HashSet: " + unique.Count);

        List<HealthCheck> sorted = new List<HealthCheck>(unique);
        sorted.Sort();
        PrintChecks("Natural order (animal, then date):", sorted);
        sorted.Sort(new HealthCheckByWeight());
        PrintChecks("By weight:", sorted);
        sorted.Sort(new HealthCheckByDate());
        PrintChecks("By date:", sorted);
        Console.WriteLine();

        // ---------- 5 ----------
        Console.WriteLine("=== 5. Exceptions and Files ===");
        Console.WriteLine("Loading animals.txt...");
        string dataPath = Path.Combine(AppContext.BaseDirectory, "animals.txt");
        Zoo loadedZoo = LoadZoo(dataPath);
        Console.WriteLine("Animals loaded: " + loadedZoo.Count);

        Console.WriteLine("Constructor check:");
        try
        {
            Lion nameless = new Lion("", "Nameless", 5.0, 10, 3);
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