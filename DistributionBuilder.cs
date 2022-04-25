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
        /// The weightsByItem to build the <see cref="IWeightedDistribution{T}"/> with.
        /// </summary>
        private readonly Dictionary<T, int> weightsByItem = new Dictionary<T, int>();

        /// <summary>
        /// Add the <paramref name="amount"/> as weight for the <paramref name="item"/>.
        /// </summary>
        public void Add(T item, int amount = 1)
        {
            var weight = this.weightsByItem.GetValueOrDefault(item);
            this.weightsByItem[item] = weight + amount;
        }

        /// <summary>
        /// Generate the <see cref="IWeightedDistribution{T}"/>.
        /// </summary>
        public IWeightedDistribution<T> ToDistribution()
        {
            var items   = this.weightsByItem.Keys.ToList();
            var weights = items.Select(item => this.weightsByItem[item]);

            return items.ToWeighted(weights);
        }
    }
}
