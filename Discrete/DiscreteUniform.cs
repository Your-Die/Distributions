using Chinchillada.Utilities;

namespace Chinchillada.Distributions
{
    using System.Collections.Generic;
    using SDU = StandardDiscreteUniform;

    public sealed class DiscreteUniform<T> : IDiscreteDistribution<T>
    {
        private readonly IDiscreteDistribution<int> _standard;

        private readonly IList<T> _support;

        public static DiscreteUniform<T> Distribution(IEnumerable<T> items) => new DiscreteUniform<T>(items);

        private DiscreteUniform(IEnumerable<T> items)
        {
            _support = items.EnsureList();
            _standard = SDU.Distribution(0, _support.Count - 1);
        }

        public T Sample()
        {
            int index = _standard.Sample();
            return _support[index];
        }

        public IEnumerable<T> Support() => _support;

        public int Weight(T variable)
        {
            return _support.Contains(variable).ToBinary();
        }

        float IWeightedDistribution<T>.Weight(T item)
        {
            return Weight(item);
        }
    }
}
