using System;

namespace NEAT
{
    class Genome : IEquatable<Genome>
    {
        // enumerated values
        public enum NodeType {Sensor, Hidden, Output}

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
                nodes.Add(new Node(NextNode(), NodeType.Sensor, this));
            }

            // create nodes for the output
            for (int i = 0; i < outputs; i++)
            {
                nodes.Add(new Node(NextNode(), NodeType.Output, this));
            }

            // create connections between the inputs and outputs
            foreach(Node x in nodes)
            {
                foreach(Node y in nodes)
                {
                    if(x.Type == NodeType.Sensor && y.Type == NodeType.Output)
                    {
                        NewConnection(x, y);
                    }
                }
            }
        }

        public void NewConnection(Node i, Node o, Nullable<double> weight = null)
        {
            if (weight == null) // if not give a weight, select a random one between -3 and 3
            {
                connections.Add(new Connection(i, o, (rand.NextDouble()*6) - 3, NextInnovation(), true));
            }
            else // otherwise, use the given value
            {
                connections.Add(new Connection(i, o, weight.Value, NextInnovation(), true));
            }
            o.Inputs.Add(i);
        }

        public int NextNode()
        {
            int temp = nextNode;
            nextNode++;
            return temp;
        }

        private int NextInnovation()
        {
            return population.NextInnovation();
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
                temp += $"    Innovation: {c.Innovation}\n    Expressed: {c.IsExpressed} | Weight: {c.Weight}\n    Input:\n\t{c.Input}\n    Output:\n\t{c.Output}\n    --------\n";
            }
            return temp;
        }

        public override String ToString()
        {
            //Genome: ####
            //    Inputs:
            //        {List of input nodes}
            //    Hidden:
            //        {List of hidden nodes}
            //    Outouts:
            //        {List of output nodes}
            return $"Genome: {this.id}\nInputs:\n{ListNodes(NodeType.Sensor)}\nHidden Nodes:\n{ListNodes(NodeType.Hidden)}\nOutput:\n{ListNodes(NodeType.Output)}\nConnections:\n{ListConnections()}";
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
            return $"Summery:\n  Inputs:\n{ListNodes(NodeType.Sensor)}\n  Hidden Nodes:\n{ListNodes(NodeType.Hidden)}\n  Output:\n{ListNodes(NodeType.Output)}\n  Connections:\n{ListConnections()}";
        }

        // Properties
        public List<Node> Nodes
        {
            get {return nodes;}
        }
        public List<Connection> Connections
        {
            get {return connections;}
        }
        public int ID
        {
            get {return this.id;}
        }
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
        public Random Rand
        {
            get {return this.rand;}
        }
    }
}