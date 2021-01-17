namespace Chinchillada.Distributions
{
    /// <summary>
    /// Interface for distributions that can be sampled.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDistribution<out T>
    {
        /// <summary>
        /// Sample the distribution.
        /// </summary>
        /// <returns>The sample.</returns>
        T Sample(IRNG random);
    }
}