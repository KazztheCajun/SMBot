using System;

namespace NEAT
{
    class Innovation : IEquatable<Innovation>
    {
        // enumerated types
        public enum IType {NODE, LINK}

        // fields
        private IType type;
        private Connection number1;
        private Connection? number2;
        private int? nodeID;
        private int? previousNumber;

        // base constructor
        public Innovation(String t, Connection c1)
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
            this.number1 = c1;
        }

        // New Node Constructor
        public Innovation(String t, Connection c1, Connection c2, Node n, int pn) :this(t, c1)
        {
            this.number2 = c2;
            this.nodeID = n.Number;
            this.previousNumber = pn;
        }

        // methods

        bool IEquatable<Innovation>.Equals(Innovation? other)
        {
            if(other == null)
            {
                return false;
            }
            
            // An Innovation is equal to another Innovation if the following are true:
            // -- NEW NODE INNOVATION:
            // -- this Innovation's first connection equals the other Innovation's other first connection
            // -- this Innovation's first connection equals the other Innovation's other second connection
            // -- this Innovation's previous number equals the other Innovation's previous number
            // -- NEW LINK INNOVATION:
            // -- this Innovation's connection equals the other Innovation's connection

            switch (this.type)
            {
                case IType.NODE:
                    if(this.number2 == null)
                    {
                        return false;
                    }

                    if(this.number1.Equals(other.Number1) && this.number2.Equals(other.Number2) && this.previousNumber == other.previousNumber)
                    {
                        return true;
                    }
                    break;

                case IType.LINK:
                    if(this.number1.Equals(other.Number1))
                    {
                        return true;
                    }
                    break;
            }
            

            return false;
        }

        bool Equals(Connection? c)
        {
            // An Innovation is equal to a Connection if the following are true:
            // -- NEW NODE INNOVATION:
            // -- the Innovation's first Connection input node equals the given Connection's input node
            // -- the Innovation's second Connection output node equals the given Connection's output node
            // -- the Innovation's previous number equals the given Connection's innovation number
            // -- NEW LINK INNOVATION:
            // -- the Innovation's first Connection equals the given Connection

            if (c == null)
            {
                return false;
            }
            
            switch (this.type)
            {
                case IType.NODE:
                    if (this.number2 == null)
                    {
                        return false;
                    }
                    if (this.number1.Input.Equals(c.Input) && this.number2.Output.Equals(c.Output) && this.previousNumber == c.Innovation)
                    {
                        return true;
                    }
                    break;
                case IType.LINK:
                    if (this.number1.Equals(c))
                    {
                        return true;
                    }
                    break;
            }

            
            return false;
        }

        // properties
        public IType Type 
        {
            get {return this.type;}
        }
        public Connection Number1
        {
            get {return this.number1;}
        }
        public Connection? Number2
        {
            get {return this.number2;}
        }
        public int? PreviousNumber
        {
            get {return this.previousNumber;}
        }
        public int? NodeID
        {
            get {return this.nodeID;}
            set {this.nodeID = value;}
        }
    }
}