using System;

namespace NEAT
{
    class Genome
    {
        // Mutation probabilites
        private static double ConnectionMutationProb = 0.25;
        private static double LinkMutationProb = 2.0;
        private static double BiasMutationProb = 0.4;
        private static double NodeMutationProb = 0.5;
        private static double EnableGeneProb = 0.2;
        private static double DisableGeneProb = 0.4;
        private static double StepSize = 0.1;

        // enumerated values
        public enum NodeType {Sensor, Hidden, Output}

        // Fields
        private List<Node> nodes;
        private List<Connection> connections;
        private int id;
        private int nextInnovation;
        private int nextNode;
        private double fitness;
        private double adjustedFitness;
        private Random rand;

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
            get {return ID;}
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

        public Genome(int id, int inputs, int outputs)
        {
            this.nodes = new List<Node>();
            this.connections = new List<Connection>();
            this.id = id;
            this.nextInnovation = 1;
            this.nextNode = 1;
            this.rand = new Random();

            // create nodes for the input
            for (int i = 0; i < inputs; i++)
            {
                nodes.Add(new Node(NewNode(), NodeType.Sensor, this));
            }

            // create nodes for the output
            for (int i = 0; i < outputs; i++)
            {
                nodes.Add(new Node(NewNode(), NodeType.Output, this));
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

        private void NewConnection(Node i, Node o)
        {
            connections.Add(new Connection(i, o, (rand.NextDouble()*6) - 3, NewInnovation(), true));
            o.Inputs.Add(i);
        }

        private int NewInnovation()
        {
            int temp = nextInnovation;
            nextInnovation++;
            return temp;
        }

        private int NewNode()
        {
            int temp = nextNode;
            nextNode++;
            return temp;
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

        public override String ToString()
        {
            return $"Genome: {id}\nInputs:\n{ListNodes(NodeType.Sensor)}\nHidden Nodes:\n{ListNodes(NodeType.Hidden)}\nOutput:\n{ListNodes(NodeType.Output)}";
        }

    }
}