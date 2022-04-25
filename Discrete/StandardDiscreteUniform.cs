namespace Chinchillada.Distributions
{
    using System.Collections.Generic;
    using System.Linq;
    using SCU = StandardContinuousUniform;
    using SDU = StandardDiscreteUniform;

    public class StandardDiscreteUniform : IDiscreteDistribution<int>
    {
        public int Minimum { get; }
        public int Maximum { get; }

        public int Size => 1 + this.Maximum - this.Minimum;

        private StandardDiscreteUniform(int minimum, int maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        public static IDiscreteDistribution<int> Distribution(int minimum, int maximum)
        {
            if (minimum > maximum)
                return Empty<int>.Distribution();

            if (minimum == maximum)
                return Singleton<int>.Distribution(minimum);

            return new SDU(minimum, maximum);
        }

        public int Sample(IRNG random)
        {
            var continuousSample = SCU.Distribution.Sample(random) * this.Size;
            return (int)(continuousSample + this.Minimum);
        }

        public IEnumerable<int> Support() => Enumerable.Range(this.Minimum, this.Size);

        public int Weight(int variable)
        {
            return this.Minimum <= variable && variable <= this.Maximum
                ? 1
                : 0;
        }

        float IWeightedDistribution<int>.Weight(int item)
        {
            return this.Weight(item);
        }

        public override string ToString()
        {
            return $"Standard Discrete Uniform[{this.Minimum}, {this.Maximum}]";
        }
    }
}