using System;

namespace NEAT
{
    class Genome : IEquatable<Genome>
    {
        // enumerated values
        public enum NodeType {SENSOR, HIDDEN, OUTPUT, BIAS}

        // Fields
        private List<Node> nodes;
        private List<Connection> connections;
        private Population population;
        private int id;
        private int nextNode;
        private double fitness;
        private double adjustedFitness;
        private Random rand;

        public Genome(int id, int inputs, int outputs, Population p, Random r)
        {
            this.nodes = new List<Node>();
            this.connections = new List<Connection>();
            this.population = p;
            this.id = id;
            this.nextNode = 1;
            this.rand = r;

            // create nodes for the input
            for (int i = 0; i < inputs; i++)
            {
                nodes.Add(new Node(NextNode(), NodeType.SENSOR, this));
            }

            // create nodes for the output
            for (int i = 0; i < outputs; i++)
            {
                nodes.Add(new Node(NextNode(), NodeType.OUTPUT, this));
            }

            // create connections between the inputs and outputs
            foreach (Node x in nodes)
            {
                foreach (Node y in nodes)
                {
                    if (x.Type == NodeType.SENSOR && y.Type == NodeType.OUTPUT)
                    {
                        Mutate.NewConnection(this, x, y, null);
                    }
                }
            }
        }

        public int NextNode()
        {
            int temp = nextNode;
            nextNode++;
            return temp;
        }

        // IEquatable override
        public bool Equals(Genome? other)
        {
            if (other is null)
            {
                return false;
            }

            if (this.id == other.ID)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        private String ListNodes(NodeType t)
        {
            String temp = "";
            foreach (Node n in nodes)
            {
                //Console.Out.WriteLine(n.Type);
                if(n.Type == t)
                {
                    temp += $"\t{n}\n";
                }
            }
            return temp;
        }

        private String ListConnections()
        {
            String temp = "";
            foreach(Connection c in connections)
            {
                temp += $"\tInnovation: {c.Innovation}\n\tExpressed: {c.IsExpressed} | Weight: {c.Weight}\n\tInput:\n\t{c.Input}\n\tOutput:\n\t{c.Output}\n\t--------\n";
            }
            return temp;
        }

        public override String ToString()
        {
            //Genome: ####
            //    Inputs:
            //        {List of input nodes}
            //    .:
            //        {List of hidden nodes}
            //    Outouts:
            //        {List of output nodes}
            return $"Genome: {this.id}\nInputs:\n{ListNodes(NodeType.SENSOR)}\nHidden Nodes:\n{ListNodes(NodeType.HIDDEN)}\nOutput:\n{ListNodes(NodeType.OUTPUT)}\nConnections:\n{ListConnections()}";
        }

        public String Summery()
        {
            //Summery:
            //    Inputs:
            //        {List of input nodes}
            //    Hidden:
            //        {List of hidden nodes}
            //    Outouts:
            //        {List of output nodes}
            return $"Summery:\n  Inputs:\n{ListNodes(NodeType.SENSOR)}\n  Hidden Nodes:\n{ListNodes(NodeType.HIDDEN)}\n  Output:\n{ListNodes(NodeType.OUTPUT)}\n  Connections:\n{ListConnections()}";
        }

        // Properties
        public List<Node> Nodes => nodes;
        public List<Connection> Connections => connections;
        public int ID => this.id;
        public double Fitness
        {
            get {return fitness;}
            set {this.fitness = value;}
        }
        public double AdjustedFitness
        {
            get {return adjustedFitness;}
            set {this.adjustedFitness = value;}
        }
        public Random Rand => this.rand;
        public Population Population => this.population;
        public int NextInnovation => population.NextInnovation();

    }
}