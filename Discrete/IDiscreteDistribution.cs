using System.Collections.Generic;

namespace Chinchillada.Distributions
{
    public interface IDiscreteDistribution<T> : IWeightedDistribution<T>
    {
        IEnumerable<T> Support();

        new int Weight(T variable);
    }
}
