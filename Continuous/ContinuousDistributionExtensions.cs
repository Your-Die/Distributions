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

        private static IDistribution<float> NormalAround(float value) => Normal.Distribution(value, 1f);
    }
}
