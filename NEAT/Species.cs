using System;

namespace NEAT
{
    class Species
    {
        // fields
        private int id;
        private int age;
        private int lastImproved;
        private double averageFitness;
        private double maxFitness;
        private double bestFitness;
        private bool isNovel;
        private bool isChecked;
        private List<Organism> organisms;

        // constructors
        public Species(int i)
        {
            this.id = i;
            this.age = 1;
            this.lastImproved = -1;
            this.averageFitness = 0;
            this.maxFitness = 0;
            this.bestFitness = 0;
            this.isNovel = false;
            this.isChecked = false;
            this.organisms = new List<Organism>();
        }

        // methods
        public void CalculateAverageFitness()
        {
            throw new NotImplementedException();
        }

        public void CalculateMaxFitness()
        {
            throw new NotImplementedException();
        }

        public void DeleteOrganism(Organism o)
        {
            throw new NotImplementedException();
        }


        // properties
        public int ID
        {
            get {return this.id;}
        }
        public int Age
        {
            get {return this.age;}
        }
        public int Size
        {
            get {return this.organisms != null ? this.organisms.Count : 0;} // if the organisms list is null, there are 0 organisms in the species
        }
        public int LastImproved
        {
            get {return this.lastImproved;}
        }
        public double AverageFitness
        {
            get {return this.averageFitness;}
        }
        public double MaxFitness
        {
            get {return this.maxFitness;}
        }
        public bool IsNovel
        {
            get {return this.isNovel;}
        }
        public bool IsChecked
        {
            get {return this.isChecked;}
        }

        public List<Organism> Organisms
        {
            get {return this.organisms;}
        }
    }
}