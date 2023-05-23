using System;

namespace NEAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Population p = new Population(2, 2, 2);
            //Console.WriteLine(p.Genomes[0]);
            Console.WriteLine($"# of Innovations: {p.Innovations.Count}");
            //Console.WriteLine(p.ToString(verbosity: true));
        }
    }

}