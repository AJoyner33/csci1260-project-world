namespace MyZooWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal nala = new Lion("LN01", "Nala", 6.0, 15, 8);
            Console.WriteLine(nala);
            Console.WriteLine(nala.MonthlyFoodCost());
            nala.Feed(5.5);
            nala.Feed(-1);
            Console.WriteLine(nala.FeedCount);
            Console.WriteLine(nala.FeedingLines());
        }
    }
}
