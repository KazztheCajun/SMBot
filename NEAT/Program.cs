using System;

namespace NEAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Genome g = new Genome(1, 2, 1, rand);
            Console.WriteLine(g);
            Mutate.AddConnection(g);
            Console.WriteLine(g);
        }
    }

}