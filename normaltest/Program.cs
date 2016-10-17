using System;
using System.Collections.Generic;

namespace normaltest
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new GaussianRandom();
            var list = new List<double>();
            double above = 0;
            double n = 1000000;
            
            for (int k = 0; k < n; k++)
            {
                double total = 0;
                for (int i = 0; i < 20; i++)
                {
                    var foo = g.NextGaussian(23000, 11000);
                    var money = Math.Floor(foo / 1000) * 10;
                    //if (money > 0)
                    {
                        total += money;
                    }
                    //Console.WriteLine($"{foo} = {money}");
                }
                if (total >= 4500 && total <= 5000)
                //if (total > 5000)
                //if (total > 5200)
                //if (total >= 4500)
                //if (total >= 4000)
                {
                    above++;
                }
            }

            
            Console.WriteLine(above / n);
            Console.WriteLine(above);

            Console.ReadLine();
        }
    }


    public sealed class GaussianRandom
    {
        private bool _hasDeviate;
        private double _storedDeviate;
        private readonly Random _random;

        public GaussianRandom(Random random = null)
        {
            _random = random ?? new Random();
        }

        /// <summary>
        /// Obtains normally (Gaussian) distributed random numbers, using the Box-Muller
        /// transformation.  This transformation takes two uniformly distributed deviates
        /// within the unit circle, and transforms them into two independently
        /// distributed normal deviates.
        /// </summary>
        /// <param name="mu">The mean of the distribution.  Default is zero.</param>
        /// <param name="sigma">The standard deviation of the distribution.  Default is one.</param>
        /// <returns></returns>
        public double NextGaussian(double mu = 0, double sigma = 1)
        {
            if (sigma <= 0)
                throw new ArgumentOutOfRangeException("sigma", "Must be greater than zero.");

            if (_hasDeviate)
            {
                _hasDeviate = false;
                return _storedDeviate * sigma + mu;
            }

            double v1, v2, rSquared;
            do
            {
                // two random values between -1.0 and 1.0
                v1 = 2 * _random.NextDouble() - 1;
                v2 = 2 * _random.NextDouble() - 1;
                rSquared = v1 * v1 + v2 * v2;
                // ensure within the unit circle
            } while (rSquared >= 1 || rSquared == 0);

            // calculate polar tranformation for each deviate
            var polar = Math.Sqrt(-2 * Math.Log(rSquared) / rSquared);
            // store first deviate
            _storedDeviate = v2 * polar;
            _hasDeviate = true;
            // return second deviate
            return v1 * polar * sigma + mu;
        }
    }
}
