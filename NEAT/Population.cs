using System;

namespace NEAT
{        
    class Population
    {
        // fields
        private int currentInnovation;
        private int currentNode;
        private List<Genome> genomes;
        private List<Species> species;
        private List<Organism> organisms;
        private List<Innovation> innovations;
        private Random rand;

        // constructors

        public Population(int size, int iSize, int oSize)
        {
            this.rand = new Random();
            this.genomes = new List<Genome>();
            this.species = new List<Species>();
            this.organisms = new List<Organism>();
            this.innovations = new List<Innovation>();
            this.currentInnovation = 1;
            this.currentNode = 1;

            for(int i = 0; i < size; i++)
            {
                genomes.Add(new Genome(i+1, iSize, oSize, this, rand));
            }
        }

        // methods
        public void Evolve()
        {
            foreach(Genome g in genomes)
            {
                if(rand.NextDouble() > .5)
                {
                    if (rand.NextDouble() > .5)
                    {
                        Mutate.AddNode(g);
                    }
                    Mutate.AddConnection(g);
                }
                
                Mutate.MutateWeights(g);
            }
        }
        public void Speciate()
        {
            throw new NotImplementedException();
        }

        public void Epoch(int nextGen)
        {
            throw new NotImplementedException();
        }

        public int NextInnovation()
        {
            int temp = currentInnovation;
            currentInnovation++;
            return temp;
        }

        public override String ToString()
        {
            // Genome: ####
            //    Total Connections: ####
            //    Total Nodes: ####
            //        -- Input: ####
            //        -- Output: ####
            //        -- Hidden: ####
            return ListGenomes();
        }

        public String ToString(bool verbosity)
        {
            // Genome: ####
            //    Total Connections: ####
            //    Total Nodes: ####
            //        -- Input: ####
            //        -- Output: ####
            //        -- Hidden: ####
            //    Summery:
            //      {Genome.Summery()}
            return ListGenomes(v: verbosity);
        }

        private String ListGenomes(bool v = false)
        {
            
            String temp = "";
            foreach(Genome g in genomes)
            {
                int ins = g.Nodes.FindAll(n => n.Type == Genome.NodeType.Sensor).Count;
                int outs = g.Nodes.FindAll(n => n.Type == Genome.NodeType.Output).Count;
                int hid = g.Nodes.FindAll(n => n.Type == Genome.NodeType.Hidden).Count;
                temp += $"Genome: {g.ID}\nTotal Connections: {g.Connections.Count}\nTotal Nodes: {g.Nodes.Count}\n  -- Input: {ins}\n  -- Output: {outs}\n  -- Hidden: {hid}\n{(v ? $"{g.Summery()}" : "\n")}";
                temp += "\n*********************************************\n";
            }
            return temp;
        }
        // properties
        public List<Genome> Genomes
        {
            get {return this.genomes;}
        }

        public List<Innovation> Innovations
        {
            get {return this.innovations;}
        }
    }

}