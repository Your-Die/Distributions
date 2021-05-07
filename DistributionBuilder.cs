using System.Collections.Generic;
using System.Linq;
using Chinchillada;

namespace Chinchillada.Distributions
{
    /// <summary>
    /// Builds instances of <see cref="IWeightedDistribution{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IWeightedDistribution{T}"/>.</typeparam>
    public sealed class DistributionBuilder<T>
    {
        /// <summary>
        /// The weights to build the <see cref="IWeightedDistribution{T}"/> with.
        /// </summary>
        private readonly Dictionary<T, int> _weights = new Dictionary<T, int>();

        /// <summary>
        /// Add the <paramref name="amount"/> as weight for the <paramref name="item"/>.
        /// </summary>
        public void Add(T item, int amount = 1)
        {
            int weight = _weights.GetValueOrDefault(item);
            _weights[item] = weight + amount;
        }

        /// <summary>
        /// Generate the <see cref="IWeightedDistribution{T}"/>.
        /// </summary>
        public IWeightedDistribution<T> ToDistribution()
        {
            var items = _weights.Keys.ToList();
            var weights = items.Select(item => _weights[item]);

            return items.ToWeighted(weights);
        }
    }
}
