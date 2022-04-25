using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Distributions
{
    [Serializable]
    public class Empty<T> : IDiscreteDistribution<T>
    {
        public static IDiscreteDistribution<T> Distribution() => new Empty<T>();

        private Empty() { }

        public T Sample(IRNG _) => throw new Exception("You cannot sample an empty distribution.");

        public IEnumerable<T> Support() => Enumerable.Empty<T>();

        public int Weight(T variable) => 0;
        float IWeightedDistribution<T>.Weight(T item) => this.Weight(item);
    }
}
