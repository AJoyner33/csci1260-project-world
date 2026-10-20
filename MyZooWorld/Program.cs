namespace MyZooWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo("Riverbend Zoo");
            zoo.Add(new Lion("LN01", "Nala", 6.0, 15, 8));
            zoo.Add(new Elephant("EL01", "Tembo", 150.0, 60, 180.0));
            zoo.Add(new Parrot("PR01", "Kiwi", 0.2, 90.0));

            Console.WriteLine(zoo.Add(new Lion("LN01", "Simba", 6.5, 14, 8)));
            Console.WriteLine(zoo.Add(null));
            Console.WriteLine(zoo.Count);
            Console.WriteLine(zoo.Find("EL01"));
            Console.WriteLine(zoo.Find("XX99") == null);
            Console.WriteLine(zoo.TrainableCount());
            Console.WriteLine(zoo.Report());
        }
    }
}
