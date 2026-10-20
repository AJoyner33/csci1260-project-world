namespace MyZooWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<HealthCheck> checks = new List<HealthCheck>();
            checks.Add(new HealthCheck("EL01", new DateTime(2026, 3, 1), 5200.0));
            checks.Add(new HealthCheck("LN01", new DateTime(2026, 2, 10), 190.5));
            checks.Add(new HealthCheck("PR01", new DateTime(2026, 1, 15), 0.9));
            checks.Add(new HealthCheck("LN01", new DateTime(2026, 2, 10), 192.0)); // duplicate key
            checks.Add(new HealthCheck("EL01", new DateTime(2026, 1, 5), 5100.0));
            checks.Add(new HealthCheck("LN01", new DateTime(2026, 1, 20), 188.0));

            HashSet<HealthCheck> unique = new HashSet<HealthCheck>(checks);
            Console.WriteLine("Loaded: " + checks.Count);
            Console.WriteLine("Unique: " + unique.Count);

            List<HealthCheck> sorted = new List<HealthCheck>(unique);

            sorted.Sort();
            Console.WriteLine("Natural order:");
            foreach (HealthCheck h in sorted) Console.WriteLine("  " + h);

            sorted.Sort(new HealthCheckByWeight());
            Console.WriteLine("By weight:");
            foreach (HealthCheck h in sorted) Console.WriteLine("  " + h);

            sorted.Sort(new HealthCheckByDate());
            Console.WriteLine("By date:");
            foreach (HealthCheck h in sorted) Console.WriteLine("  " + h);
        }
    }
}
