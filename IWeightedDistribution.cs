namespace Chinchillada.Distributions
{
    /// <summary>
    /// Interface for <see cref="IDistribution{T}"/> where the items in the distribution are weighted.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWeightedDistribution<T> : IDistribution<T>
    {
        /// <summary>
        /// Get the weight of the <paramref name="item"/>.
        /// </summary>
        float Weight(T item);
    }
}
