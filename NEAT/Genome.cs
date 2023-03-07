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
            foreach(Node x in nodes)
            {
                foreach(Node y in nodes)
                {
                    if(x.Type == NodeType.SENSOR && y.Type == NodeType.OUTPUT)
                    {
                        NewConnection(x, y);
                    }
                }
            }
        }

        public void NewConnection(Node i, Node o, bool testInnovation, Nullable<double> weight = null, Nullable<int> inum = null)
        {
            Connection temp; // construct a new Connection from the given information
            if (weight == null) // if not give a weight, select a random one between -3 and 3
            {
                temp = new Connection(i, o, (rand.NextDouble()*6) - 3, 0, true);
            }
            else // otherwise, use the given value
            {
                temp = new Connection(i, o, weight.Value, 0, true);
            }

            if(inum != null)
            {
                temp.Innovation = (int) inum;
            }

            if(testInnovation)
            {
                CheckInnovation(temp);
            }
            
            connections.Add(temp); // add the connection gene to the list of connection genes
            o.Inputs.Add(i); // add input node to the output node's input list
        }

        private void CheckInnovation(Connection c)
        {
            // check if this connection is a novel innovation or not
            bool isNovel = false;
            foreach(Innovation n in this.population.Innovations)
            {
                if(n.Equals(c))
                {
                    c.Innovation = n.Number1.Innovation; // give it the existing innovation number
                    isNovel = true;
                    break;
                }
            }

            if(!isNovel) // if no equivalent Innovation is found
            {
                c.Innovation = NextInnovation(); // give it the next available innovation number
            }
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

        public Population Population
        {
            get {return this.population;}
        }
    }
}