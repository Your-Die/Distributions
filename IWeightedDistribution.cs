using Chinchillada.Distributions;

namespace Chinchillada.Distributions
{
    public interface IWeightedDistribution<T> : IDistribution<T>
    {
        float Weight(T item);
    }
}
