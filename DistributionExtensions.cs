namespace Chinchillada.Distributions
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension method container for <see cref="IDistribution{T}"/>.
    /// </summary>
    public static class DistributionExtensions
    {
        /// <summary>
        /// Infinite enumerable of samples from this <see cref="IDistribution{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of items in this <see cref="IDistribution{T}"/></typeparam>
        /// <param name="distribution">The distribution we're sampling.</param>
        /// <returns>The infinite <see cref="IEnumerable{T}"/> of samples.</returns>
        public static IEnumerable<T> Samples<T>(this IDistribution<T> distribution)
        {
            while (true)
            {
                yield return distribution.Sample();
            }
        }
    }
}
