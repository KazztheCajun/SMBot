using System;

namespace NEAT
{
    class Mutate
    {
        // Mutation probabilites
        private static double ConnectionMutationProb = 0.25;
        private static double LinkMutationProb = 2.0;
        private static double BiasMutationProb = 0.4;
        private static double NodeMutationProb = 0.5;
        private static double EnableGeneProb = 0.2;
        private static double DisableGeneProb = 0.4;
        private static double StepSize = 0.1;

        // Helper functions
        public static void AddConnection(Genome g)
        {
            while (true) // loop until a new connection is made
            {
                Node node = g.Nodes[g.Rand.Next(g.Nodes.Count)]; // select a random node
                List<Node> unconnected = g.Nodes.FindAll(n => !n.Inputs.Contains<Node>(node)); // find all other nodes that it is not connected to
                if (unconnected.Count > 0) // if there are any unconnected nodes
                {
                    Node other = unconnected[g.Rand.Next(unconnected.Count)]; // select a random one
                    g.NewConnection(node, other); // create a new connection gene
                    break; // exit the loop
                }
            }
        }

        public static void AddNode(Genome g)
        {
            // select a random connection
            Connection c = g.Connections[g.Rand.Next(g.Connections.Count)];
            while (!c.IsExpressed) // if the connection is disabled, select a new connection
            {
                c = g.Connections[g.Rand.Next(g.Connections.Count)];
            }
            c.IsExpressed = false; // disable the old connection
            Node n = new Node(g.NextNode(), Genome.NodeType.Hidden, g); // create a new node to inside the old connection
            g.Nodes.Add(n); // add the new node to the list of nodes in the Genome
            g.NewConnection(c.Input, n); // create a new connection between the old input and the new node
            g.NewConnection(n, c.Output, weight: c.Weight); // create a new connection between the new node and the old output
        }

        public static void DisableGene(Genome g)
        {
            // get a list of active genes
            List<Connection> active = g.Connections.FindAll(c => c.IsExpressed);
            // select a gene randomly
            Connection connection = active[g.Rand.Next(active.Count-1)];
            // check that this gene can be disabled without isolating it's subnetwork
            // to do this, a connection must have the following properties:
            // -- The input node of the connection has at least one other connection for which it is also an input node
            // -- At least on of the other connections is not currently disabled
            // -- The innovation number of the other connections are not the same as this connection
            List<Connection> hasOther = g.Connections.FindAll(c => c.Input.Equals(connection.Input) || c.IsExpressed || c.Innovation != connection.Innovation);
            if(hasOther.Count > 0) // if there is at least one other connection
            {
                connection.IsExpressed = false; // the connection can be safely disabled
            }
        }

        public static void EnableGene(Genome g)
        {
            // select a random disabled gene
            List<Connection> disabled = g.Connections.FindAll(c => !c.IsExpressed);

            if(disabled.Count > 0)
            {
                disabled[g.Rand.Next(disabled.Count - 1)].IsExpressed = true;
            }

        }

        public static void MutateWeights(Genome g, String t = "noise")
        {
            // mutate each weight in a genome
            foreach(Connection c in g.Connections)
            {
                if(t == "noise")
                {
                    GaussianNoise(c);
                }
            }
        }

        private static void GaussianNoise(Connection c)
        {
            // adds gaussian noise to the weight of a Connection
            c.Weight += Helper.NextGaussian();
        }

        private static void ColdGaussian(Connection c)
        {
            // sets the weight of a connection to a normal random number
            c.Weight = Helper.NextGaussian();
        }
 
    }
}