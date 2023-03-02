using System;

namespace NEAT
{
    class Helper
    {
        // fields
        private static double lastGauss;
        private static bool hasGauss;
        private static Random rand = new Random();

        // methods
        public static double NextGaussian()
        {
            if(hasGauss) // if a value has already been calculated
            {
                hasGauss = false;
                return lastGauss;
            }
            else // otherwise caluculate a new pair of values
            {
                // Box-Muller Polar method for normally distributed random number generation: https://mathworld.wolfram.com/Box-MullerTransformation.html
                // Generates a new pair of random numbers every two calls
                double f, x1, x2, r2;

                do
                {
                    x1 = 2.0 * rand.NextDouble() - 1.0;
                    x2 = 2.0 * rand.NextDouble() - 1.0;
                    r2 = x1 * x1 + x2 * x2;
                } while (r2 >= 1.0 || r2 == 0.0);

                f = Math.Sqrt(-2.0 * Math.Log(r2) / r2);
                lastGauss = f * x1;
                hasGauss = true;
                return f * x2;
            }
            
        }
    }
    
}