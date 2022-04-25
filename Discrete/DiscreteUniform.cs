namespace Chinchillada.Distributions
{
    using System.Collections.Generic;
    using System.Linq;
    using SDU = StandardDiscreteUniform;

    public sealed class DiscreteUniform<T> : IDiscreteDistribution<T>
    {
        private readonly IDiscreteDistribution<int> standard;

        private readonly IReadOnlyList<T> support;

        public static DiscreteUniform<T> Distribution(IReadOnlyList<T> support) => new DiscreteUniform<T>(support);

        private DiscreteUniform(IReadOnlyList<T> support)
        {
            this.support  = support;
            this.standard = SDU.Distribution(0, this.support.Count - 1);
        }

        public T Sample(IRNG random)
        {
            int index = this.standard.Sample(random);
            return this.support[index];
        }

        public IEnumerable<T> Support() => this.support;

        public int Weight(T variable)
        {
            return this.support.Contains(variable).ToBinary();
        }

        float IWeightedDistribution<T>.Weight(T item) => this.Weight(item);
    }
}
