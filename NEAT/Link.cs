using System;

namespace NEAT
{
    public class Link : IEquatable<Link>
    {
        private Node input;
        private Node output;
        private double weight;
        private bool isDelayed;

        public Node Input => input;
        public Node Output => output;
        public double Weight
        {
            get { return weight; }
            set { this.weight = value; }
        }
        public bool IsDelayed => isDelayed;

        public Link(Node i, Node o, double w)
        {
            this.input = i;
            this.output = o;
            this.weight = w;
            this.isDelayed = false;
        }

        public bool Equals(Link? other)
        {
            if (other == null) return false;

            if(this.input.Equals(other.Input) && this.output.Equals(other.Output))
            {
                return true;
            }

            return false;
        }
    }
}