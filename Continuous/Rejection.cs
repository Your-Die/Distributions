using System;

namespace Chinchillada.Distributions
{
    public class Rejection<T> : IWeightedDistribution<T>
    {
        private readonly Func<T, float>           weightFunction;
        private readonly IWeightedDistribution<T> helper;
        private readonly float                    factor;

        public static IWeightedDistribution<T> Distribution(
            Func<T, float>           weightFunction,
            IWeightedDistribution<T> helper,
            float                    factor = 1)
        {
            return new Rejection<T>(weightFunction, helper, factor);
        }

        private Rejection(Func<T, float> weightFunction, IWeightedDistribution<T> helper, float factor)
        {
            this.weightFunction = weightFunction;

            this.helper = helper;
            this.factor = factor;
        }

        public T Sample(IRNG random)
        {
            while (true)
            {
                var sample       = this.helper.Sample(random);
                var helperWeight = this.helper.Weight(sample) * this.factor;

                var weight = this.Weight(sample);

                if (Flip.Boolean(weight / helperWeight).Sample(random))
                    return sample;
            }
        }

        public float Weight(T item) => this.weightFunction(item);
    }
}