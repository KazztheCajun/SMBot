using System;

namespace NEAT
{
    class Organism
    {
        // fields
        private double fitness;
        private double previousFitness;
        private Network network;
        private Genome genome;
        private Species? species;
        private int offspring;
        private int generation;
        private bool isDead;
        private bool isChampion;

        // constructors
        public Organism(int fit, Genome genome, int generation)
        {
            this.fitness = fit;
            this.previousFitness = fit;
            this.genome = genome;
            this.generation = generation;
            this.species = null;
            this.isDead = false;
            this.isChampion = false;
            this.network = Network.Genesis(genome, genome.ID);
        }

        public void UpdatePhenotype()
        {
            throw new NotImplementedException();
        }

        // methods

        // properties
    
    }
}