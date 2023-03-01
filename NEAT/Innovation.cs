using System;

namespace NEAT
{
    class Innovation : IEquatable<Innovation>
    {
        // enumerated types
        public enum IType {NODE, LINK}

        // fields
        private IType type;
        private Node input;
        private Node output;
        private Connection? number1;
        private Connection? number2;
        private int? previousNumber;

        // base constructor
        public Innovation(String t, Node i, Node o)
        {
            switch (t)
            {
                case "link":
                    this.type = IType.LINK;
                    break;
                case "node":
                    this.type = IType.NODE;
                    break;
            }
            
            this.input = i;
            this.output = o;
        }

        // New Link Constructor
        public Innovation(String t, Node i, Node o, Connection c1) :this(t, i, o)
        {
            number1 = c1;
        }

        // New Node Constructor
        public Innovation(String t, Node i, Node o, Connection c1, Connection c2, int pn) :this(t, i, o)
        {
            this.number1 = c1;
            this.number2 = c2;
            this.previousNumber = pn;
        }
        

        // properties
        public IType Type 
        {
            get {return this.type;}
        }

        public Node Input
        {
            get {return this.input;}
        }

        public Node Output
        {
            get {return this.output;}
        }

        public Connection Number1
        {
            get {return this.number1;}
        }

        public Connection Number2
        {
            get {return this.number2;}
        }

        public int PreviousNumber
        {
            get {return this.previousNumber;}
        }

        bool IEquatable<Innovation>.Equals(Innovation? other)
        {
            if(other == null)
            {
                return false;
            }

            if(this.type == other.Type && this.input.Equals(other.input) && this.output.Equals(other.output))
            {
                return true;
            }

            return false;
        }

        bool Equals(Connection? c)
        {
            if(c == null)
            {
                return false;
            }

            if(this.input.Equals(c.Input) && this.output.Equals(c.Output) && this.number1.Innovation == c.Innovation)
            {
                return true;
            }

            return false;
        }
    }
}