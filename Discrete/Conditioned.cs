using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada;

namespace Chinchillada.Distributions
{
    public class Conditioned<T> : IDiscreteDistribution<T>
    {
        private readonly List<T> support;
        private readonly IDiscreteDistribution<T> underlying;
        private readonly Func<T, bool> predicate;

        public static IDiscreteDistribution<T> Distribution(
            IDiscreteDistribution<T> underlying,
            Func<T, bool> predicate)
        {
            var conditioned = new Conditioned<T>(underlying, predicate);
            var support = conditioned.support;

            switch (support.Count)
            {
                case 0:
                    throw new ArgumentException();
                case 1:
                    return Singleton<T>.Distribution(support.First());
                default:
                    return conditioned;
            }
        }

        private Conditioned(IDiscreteDistribution<T> underlying, Func<T, bool> predicate)
        {
            this.underlying = underlying;
            this.predicate     = predicate;
            this.support = underlying.Support()
                                      .Where(predicate)
                                      .ToList();
        }

        public T Sample(IRNG random)
        {
            Func<T> sampleFunction = () => this.underlying.Sample(random);
            return sampleFunction.Until(this.predicate);
        }

        public IEnumerable<T> Support()
        {
            return this.support.AsEnumerable();
        }

        public int Weight(T variable)
        {
            return this.predicate(variable) ? this.underlying.Weight(variable) : 0;
        }

        float IWeightedDistribution<T>.Weight(T item)
        {
            return this.Weight(item);
        }
    }
}
