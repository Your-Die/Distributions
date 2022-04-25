using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada;

namespace Chinchillada.Distributions
{
    public class Projected<A, R> : IDiscreteDistribution<R>
    {
        private readonly IDiscreteDistribution<A> underlying;

        private readonly Func<A, R> projection;

        private readonly Dictionary<R, int> weights;

        public static IDiscreteDistribution<R> Distribution(IDiscreteDistribution<A> underlying, Func<A, R> projection)
        {
            var result = new Projected<A, R>(underlying, projection);

            if (result.weights.Count == 0)
                return Empty<R>.Distribution();

            var support = result.Support().ToList();
            if (support.Count == 1)
                return Singleton<R>.Distribution(support.First());

            return result;
        }

        private Projected(IDiscreteDistribution<A> underlying, Func<A, R> projection)
        {
            this.underlying = underlying;
            this.projection = projection;

            this.weights = underlying.Support()
                                     .GroupBy(projection, underlying.Weight)
                                     .ToDictionary(group => group.Key, group => group.Sum());
        }

        public R Sample(IRNG random)
        {
            var underlyingSample = this.underlying.Sample(random);
            return this.projection(underlyingSample);
        }

        public IEnumerable<R> Support() => this.weights.Keys;

        public int Weight(R variable) => this.weights.GetValueOrDefault(variable);
        float IWeightedDistribution<R>.Weight(R item) => this.Weight(item);
    }
}
