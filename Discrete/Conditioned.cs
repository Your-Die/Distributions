using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Distributions
{
    public class Conditioned<T> : IDiscreteDistribution<T>
    {
        private readonly List<T> support;

        private readonly IDiscreteDistribution<T> underlying;

        private readonly Func<T, bool> predicate;

        public static IDiscreteDistribution<T> Distribution(
            IDiscreteDistribution<T> underlying,
            Func<T, bool>            predicate)
        {
            var conditioned = new Conditioned<T>(underlying, predicate);
            var support     = conditioned.support;

            return support.Count switch
            {
                0 => throw new ArgumentException(),
                1 => Singleton<T>.Distribution(support.First()),
                _ => conditioned
            };
        }

        private Conditioned(IDiscreteDistribution<T> underlying, Func<T, bool> predicate)
        {
            this.underlying = underlying;
            this.predicate  = predicate;
            this.support = underlying.Support()
                                     .Where(predicate)
                                     .ToList();
        }

        public T Sample(IRNG random)
        {
            Func<T> sampleFunction = () => this.underlying.Sample(random);
            return sampleFunction.Until(this.predicate);
        }

        public IEnumerable<T> Support() => this.support;

        public int Weight(T variable) => this.predicate(variable) ? this.underlying.Weight(variable) : 0;

        float IWeightedDistribution<T>.Weight(T item) => this.Weight(item);
    }
}