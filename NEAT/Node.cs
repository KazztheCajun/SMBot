using System;

namespace NEAT
{
    class Node : IEquatable<Node>
    {

        // fields
        private Genome.NodeType type;
        private List<Node> inputs;
        private double potential;
        private double activation;
        private Genome genome;
        private int number;

        // properties
        public int Number => number;
        public double Potential => potential;
        public List<Node> Inputs => inputs;
        public Genome.NodeType Type
        {
            get {return type;}
            set {type = value;}
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
            this.activation = ReLU(potential);
        }

        public double ReLU(double value) // Uses rectified Linear Unit as the activation function
        {
            return Math.Max(value, 0.0);
        }

        public double Sigmoid(double value)
        {
            double slope = 4.924273;
            return (1 / (1 + (Math.Exp(-(slope * value)))));
        }

        public bool Equals(Node? other)
        {
            if (other is null)
            {
                return false;
            } 

            if(this.number == other.Number)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return number.GetHashCode();
        }

        public override string ToString()
        {
            return $"Node: {number} | Type: {type} | Inputs: {inputs.Count}";
        }
    }
}