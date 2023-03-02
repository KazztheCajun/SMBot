using System;

namespace NEAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Population p = new Population(4, 2, 2);
            //Console.WriteLine(p.Genomes[0]);
            Console.WriteLine(p);
            Console.WriteLine(p.ToString(verbosity: true));
        }
    }

}