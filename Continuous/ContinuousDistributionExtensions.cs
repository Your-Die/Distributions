using System.Linq;
using Chinchillada.Utilities;
using Rewired;

namespace Chinchillada.Distributions
{
    using System;

    public static class ContinuousDistributionExtensions
    {
        public static Metropolis<float> NormalMetropolis(this Func<float, float> weight)
        {
            return Metropolis<float>.Distribution(weight, Normal.Standard, NormalAround);

        }

        public static Func<T, IWeightedDistribution<float>> Posterior<T>(
            this IWeightedDistribution<float> prior,
            Func<float, IWeightedDistribution<T>> likelihood)
        {
            return value =>
            {
                return Metropolis<float>.Distribution(Bayes, prior, NormalAround);

                float Bayes(float d) => prior.Weight(d) * likelihood(d).Weight(value);
            };
        }

        public static float ExpectedValue(
            this IWeightedDistribution<float> distribution,
            Func<float, float> function,
            float start = 0f,
            float end = 1f,
            int buckets = 1000)
        {
            Func<float, float> value = x => function(x) * distribution.Weight(x);
            Func<float, float> weight = distribution.Weight;

            return value.Area(start, end, buckets) / weight.Area(start, end, buckets);
        }

        private static IDistribution<float> NormalAround(float value) => Normal.Distribution(value, 1f);
    }
}
