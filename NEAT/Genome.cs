using System;

namespace NEAT
{
    public class Genome : IEquatable<Genome>
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

        /* 
         * Basic constructor that creates a new, minimal genome with the given number of inputs and outputs
         * Params:
         * id => the id number for this Genome, generally from the Population associated with it
         * inputs => the number of input, or SENSOR, nodes for this Genome
         * outputs => the number of OUTPUT nodes for this Genome
         * p => the Population that spawned this Genome
         * r => reference to the random generator for the given Population
         *
         */

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

        /*
         *
         * Constructor that creates a new, blank Genome
         *
         */
        public Genome(int id, Population p, Random r)
        {
            this.id = id;
            this.nodes = new List<Node>();
            this.connections = new List<Connection>();
            this.population = p;
            this.rand = r;
            this.nextNode = 1;
        }

        // find all non-SENSOR nodes that are not connected to the given node
        // should also filter to not allow output nodes to connect to other output nodes
        public List<Node> FindUnconnectedNodes(Node node)
        {
            List<Node> connected = new List<Node>();

            foreach (Connection c in connections) // check each connection
            {
                if (c.Input.Equals(node)) // if the given node is listed as an input, add the corresponding output node
                {
                    connected.Add(c.Output);
                }
                else if (c.Input.Type != NodeType.SENSOR && c.Output.Equals(node)) // if the given node is listed as output and the input node is not a SENSOR, add the corresponding input node
                {
                    connected.Add(c.Input);
                }
            }

            return nodes.FindAll((n) => !connected.Contains(n) && n.Type != NodeType.SENSOR); // return the list of nodes that are not in the list of connected nodes

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