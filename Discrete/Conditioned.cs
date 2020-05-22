using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;

namespace Chinchillada.Distributions
{
    public class Conditioned<T> : IDiscreteDistribution<T>
    {
        private readonly List<T> _support;
        private readonly IDiscreteDistribution<T> _underlying;
        private readonly Func<T, bool> _predicate;

        public static IDiscreteDistribution<T> Distribution(
            IDiscreteDistribution<T> underlying,
            Func<T, bool> predicate)
        {
            var conditioned = new Conditioned<T>(underlying, predicate);
            var support = conditioned._support;

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
            _underlying = underlying;
            _predicate = predicate;
            _support = underlying.Support()
                .Where(predicate)
                .ToList();
        }

        public T Sample()
        {
            Func<T> sampleFunction = _underlying.Sample;
            return sampleFunction.Until(_predicate);
        }

        public IEnumerable<T> Support()
        {
            return _support.AsEnumerable();
        }

        public int Weight(T variable)
        {
            return _predicate(variable) ? _underlying.Weight(variable) : 0;
        }

        float IWeightedDistribution<T>.Weight(T item)
        {
            return Weight(item);
        }
    }
}
