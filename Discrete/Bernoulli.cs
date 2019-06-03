namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SCU = StandardContinuousUniform;

    public class Bernoulli : IDiscreteDistribution<int>
    {
        private Bernoulli(int zeroes, int ones)
        {
            Zeroes = zeroes;
            Ones = ones;
        }

        public int Zeroes { get; }
        public int Ones { get; }

        public float ZeroChance => (float)Zeroes / (Zeroes + Ones);

        public static IDiscreteDistribution<int> Distribution(int zeroes, int ones)
        {
            if (zeroes < 0 || ones < 0)
                throw new ArgumentException();
            if (zeroes == 0 && ones == 0)
                return Empty<int>.Distribution();
            if (zeroes == 0)
                return Singleton<int>.Distribution(1);
            if (ones == 0)
                return Singleton<int>.Distribution(0);

            return new Bernoulli(zeroes, ones);
        }

        public int Sample() => SCU.Distribution.Sample() <= this.ZeroChance ? 0 : 1;

        public IEnumerable<int> Support() => Enumerable.Range(0, 2);

        public int Weight(int variable)
        {
            switch (variable)
            {
                case 0:
                    return this.Zeroes;
                case 1:
                    return this.Ones;
                default:
                    return 0;
            }
        }

        float IWeightedDistribution<int>.Weight(int item) => Weight(item);
    }
}
