using System;

namespace NEAT
{
    class Connection : IEquatable<Connection>
    {
        // fields
        Node input;
        Node output;
        double weight;
        bool isExpressed;
        int innovation;

        // constructors
        public Connection(Node i, Node o, double w, int inum, bool e)
        {
            this.input = i;
            this.output = o;
            this.weight = w;
            this.innovation = inum;
            this.isExpressed = e;
        }
        
        // methods
        bool IEquatable<Connection>.Equals(Connection? other)
        {
            if(other == null)
            {
                return false;
            }

            if(this.input.Equals(other.Input) && this.output.Equals(other.Output))
            {
                return true;
            }

            return false;
        }

        // properties
        public Node Input
        {
            get {return input;}
        }
        public Node Output
        {
            get {return output;}
        }
        public double Weight
        {
            get {return weight;}
            set {this.weight = value;}
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