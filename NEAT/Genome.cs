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
                        NewConnection(x, y, null);
                    }
                }
            }
        }
        public Connection NewConnection(Node i, Node o, Nullable<int> inum,  Nullable<double> weight = null)
        {
            Connection temp; // construct a new Connection from the given information
            if (weight == null) // if not given a weight, select a random one between -3 and 3
            {
                temp = new Connection(i, o, (rand.NextDouble()*6) - 3, 0, true);
            }
            else // otherwise, use the given value
            {
                temp = new Connection(i, o, weight.Value, 0, true);
            }

            if(inum != null) // if given an innovation number, use that one
            {
                temp.Innovation = (int) inum;
            }
            else // otherwise, check the Population's list of innovations and assin accordingly
            {
                CheckInnovation(temp);
            }
            
            connections.Add(temp); // add the connection gene to the list of connection genes
            o.Inputs.Add(i); // add input node to the output node's input list
            return temp;
        }

        private void CheckInnovation(Connection c)
        {
            // check if this connection is a novel innovation or not
            bool isNovel = true;
            // for each link innovation in the population
            var list = this.population.Innovations.FindAll(x => x.Type == Innovation.IType.LINK);
            Console.WriteLine(list.Count);
            foreach(Innovation i in list)
            {
                Console.WriteLine($"Checking connection:\nInnovation #: {i.Number1.Innovation} -> ({i.Number1.Input.Number}, {i.Number1.Output.Number}) vs ({c.Input.Number}, {c.Output.Number}) | {i.EqualsConnection(c)}");
                if(i.EqualsConnection(c))
                {
                    
                    c.Innovation = i.Number1.Innovation; // give it the existing innovation number
                    isNovel = false;
                    break;
                }
            }

            if(isNovel) // if no equivalent Innovation is found
            {
                c.Innovation = NextInnovation; // give it the next available innovation number
                this.population.Innovations.Add(new Innovation("link", c));
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