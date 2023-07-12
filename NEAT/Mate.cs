using System;

namespace NEAT
{

    public class Mate
    {
        // Take two incoming genomes and return an offspring genome
         public static Genome Offspring(Genome g1, Genome g2, Population p)
         {
            int size;
            Genome g = new Genome(p.NextGenome(), p, p.Random);
            List<Node> newNodes = new List<Node>();
            List<Connection> newCons = new List<Connection>();

            // construct the new list of nodes and connections
            if(g1.Fitness > g2.Fitness)
            {
                CheckConnections(g1, g2, ref newNodes, ref newCons);
            }
            else if (g1.Fitness < g2.Fitness)
            {
                CheckConnections(g2, g1, ref newNodes, ref newCons);
            }
            else // if fitness is equal, choose the smallest Genome to encourage minimal structures
            {
                if(g1.Connections.Count <= g1.Connections.Count) // if they are the same size, just go in order
                {
                    CheckConnections(g1, g2, ref newNodes, ref newCons);
                }
                else
                {
                    CheckConnections(g2, g1, ref newNodes, ref newCons);
                }
            }

            // once nodes and connections are selected, add them to the new child Genome
            foreach (Node n in newNodes)
            {
                g.Nodes.Add(n);
            }

            foreach (Connection c in newCons)
            {
                g.Connections.Add(c);
            }

            return g;
        }

        /* 
         * compare the connections in both genomes and add to the child if the following:
         * 1) If both Genomes have it, then add the connection and the nodes involved. (Given the child doesn't already have it)
         * 2) If only one Genome has it, only add it if the parent if comes from is the more fit
         *
         * Genome one is the more fit genome, so we loop over it's connections to get any excess or disjointed connections and ignore the less fit ones
         */
        private static void CheckConnections(Genome one, Genome two, ref List<Node> nodeList, ref List<Connection> conList)
        {
            // loop over more fit genomes connections
            foreach (Connection c in one.Connections) 
            {
                // if they both contain it, add the nodes and connection to the new lists
                if(two.Connections.Contains(c))
                {   
                    // randomly choose a connection from the two Genomes
                    #pragma warning disable CS8600 // cannot be null because of above condition
                    Connection second = one.Connections.Find((c1) => c1.Equals(c));

                    // create a new copy of the selected connection and add it 
                    
                }
                else // otherwise add the disjoint or excess connection, and the nodes if applicable
                {
                    // create a new copy of the connection and it's node
                    // add the connection
                    // check if the nodes already exist and, if not, add them
                }

            }
        }



    }


}
