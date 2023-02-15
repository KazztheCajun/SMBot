using System;

namespace NEAT
{
    class Node
    {

        // fields
        private Genome.NodeType type;
        private List<Node> inputs;
        private double potential;
        private Genome genome;
        private int number;

        // properties
        public int Number
        {
            get {return number;}
        }
        public double Potential
        {
            get {return potential;}
        }

        public Genome.NodeType Type
        {
            get {return type;}
            set {type = value;}
        }

        public List<Node> Inputs
        {
            get {return inputs;}
        }

        public Node(int n, Genome.NodeType t, Genome g)
        {
            this.type = t;
            this.inputs = new List<Node>();
            this.potential = 0;
            this.genome = g;
            this.number = n;
        }

        public void CalculatePotential()
        {
            // Calculate the activation potential of this node
            double potential = 0;
            List<Connection> connections = genome.Connections.FindAll(c => c.Output.Equals(this)); // find all connections that this node is the output of
            foreach (Connection c in connections) // foreach connection
            {
                if(c.IsExpressed) // if the gene is expressed
                {
                    potential += c.Input.potential * c.Weight; // add its potential * weight to the activation sum
                }
            }

            // pass the resulting sum into the activation function
            this.potential = Activation(potential);
        }

        public double Activation(double value) // Uses rectified Linear Unit as the activation function
        {
            return Math.Max(value, 0.0);
        }

        public bool Equals(Node other)
        {
            if(this.number == other.Number)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Node: {number} | Type: {type} | Inputs: {inputs.Count}";
        }
    }
}