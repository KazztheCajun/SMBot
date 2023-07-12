using System;

namespace NEAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Population p = new Population(2, 2, 2); // size, inputs per genome, outputs per genome
            //Console.WriteLine(p.ToString(verbosity: true));
            foreach(Genome g in p.Genomes)
            {
                //Mutate.AddNode(g);
                //Mutate.AddNode(g);
                Mutate.AddConnection(g);
                Mutate.AddConnection(g);
            }
            //Console.WriteLine(p.Genomes[0]);
            //Console.WriteLine($"# of Innovations: {p.Innovations.Count}");
            Console.WriteLine(p.ToString(verbosity: true));
        }
    }

}