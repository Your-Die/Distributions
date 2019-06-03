using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;

namespace Chinchillada.Distributions
{
    public sealed class DistributionBuilder<T>
    {
        private readonly Dictionary<T, int> _weights = new Dictionary<T, int>();

        public void Add(T item, int amount = 1)
        {
            int weight = _weights.GetValueOrDefault(item);
            _weights[item] = weight + amount;
        }

        public IDistribution<T> ToDistribution()
        {
            var items = _weights.Keys.ToList();
            var weights = items.Select(item => _weights[item]);

            return items.ToWeighted(weights);
        }
    }
}
