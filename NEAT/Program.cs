using System;

namespace NEAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Genome g = new Genome(1, 2, 1);
            Console.WriteLine(g);
            Mutate.AddConnection(g);
            Console.WriteLine(g);
        }
    }

}