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

        public Population(int size)
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
                genomes.Add(new Genome(i+1, 4, 4, this, rand));
            }
        }

        // methods
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
        // properties
    }

}