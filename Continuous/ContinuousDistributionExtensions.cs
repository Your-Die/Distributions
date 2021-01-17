using Chinchillada.Foundation;

namespace Chinchillada.Distributions
{
    using System;

    public static class ContinuousDistributionExtensions
    {
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