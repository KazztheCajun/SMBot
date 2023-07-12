using System;

namespace NEAT
{
    public class Connection : IEquatable<Connection>
    {
        // fields
        Link link;
        bool isExpressed;
        int innovation;

        // constructors
        public Connection(Node i, Node o, double w, int inum, bool e)
        {
            this.link = new Link(i, o, w);
            this.innovation = inum;
            this.isExpressed = e;
        }
        
        // methods
        public bool Equals(Connection? other)
        {
            if(other == null)
            {
                Console.WriteLine("is the connection null");
                return false;
            }

            return this.link.Equals(other.Link);
        }

        public bool SameInnovation(Connection? other)
        {
            if (other == null) return false;

            return this.innovation.Equals(other.Innovation);
        }

        // properties
        public Node Input
        {
            get {return link.Input;}
        }
        public Node Output
        {
            get {return link.Output;}
        }
        public Link Link => link;
        public double Weight
        {
            get {return link.Weight;}
            set {this.link.Weight = value;}
        }
        public bool IsExpressed
        {
            get {return isExpressed;}
            set {this.isExpressed = value;}
        }
        public int Innovation
        {
            get {return innovation;}
            set {this.innovation = value;}
        }
    }
}