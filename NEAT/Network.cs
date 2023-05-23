using System;

namespace NEAT
{
    class Network
    {
        // fields
        private Genome genome;
        private String name;
        private List<Node> inputs;
        private List<Node> outputs;
        private int id;

        // constructors
        public Network(Genome g, int netID, String name)
        {
            this.genome = g;
            this.name = name;
            this.inputs = genome.Nodes.FindAll(n => n.Type == Genome.NodeType.SENSOR);
            this.outputs = genome.Nodes.FindAll(n => n.Type == Genome.NodeType.OUTPUT);
            this.id = netID;
        }

        // methods

        public static Network Genesis(Genome g, int id, String name = "")
        {
            // factory method to generate a new network based on a given genome
            Network temp = new Network(g, id, name);

            return temp;

        }

        public void Activate()
        {

        }

        public void ShowActivation()
        {

        }

        public void LoadSensors()
        {

        }

        public int MaxDepth()
        {
            int max = 0;
            int current = 0; // initialize the current max path
            foreach(Node n in genome.Nodes)
            {
                if(n.Type == Genome.NodeType.OUTPUT) // start at the output node
                {
                    // set the max depth of an output node to the longest chain of connected nodes ending with that output node, excluding the input and output node of that chain
                    current = Depth(0, n);  
                    if(current > max)
                    {
                        max = current; // if the current output node has a larger depth value than the overall max, set it to the overall max of the network
                    }
                }
            }
            return max; // return the maximum depth in this network
        }

        private int Depth(int d, Node n)
        {
            // recursivly walk down the list of nodes connected to the given node and return the depth when an input node is hit
            int max = d; // set max depth for this node to the current max depth
            if(n.Type == Genome.NodeType.SENSOR) // recursivly walk down the list of connected nodes to the end
            {
                return d; // return the current depth if the current node is an input node
            }

            List<Connection> list = genome.Connections.FindAll(c => c.Output.Equals(n)); // otherwise, find all nodes the current node is connected to
            int current = 0; // initialize the current max
            foreach(Connection c in list)
            {
                current = Depth(d + 1, c.Input);  // incriment the depth and recurse with the input node as the node
                if(current > max) // if the current max is greater than the global max
                {
                    max = current; // set the global max to the current max
                }
            }
            
            return max; // return the global max
        }

        // properties
        public Genome Genome => this.genome;
        public int Nodes => this.genome.Nodes.Count;
        public int Links => this.genome.Connections.Count;
        public String Name
        {
            get {return this.name;}
            set {this.name = value;}
        }
        public List<Node> Inputs => this.inputs;
        public List<Node> Outputs => this.outputs;
        public int ID => this.id;

    }
}