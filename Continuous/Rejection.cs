using System;

namespace Chinchillada.Distributions
{
    public class Rejection<T> : IWeightedDistribution<T>
    {
        private readonly Func<T, float> _weightFunction;
        private readonly IWeightedDistribution<T> _helper;
        private readonly float _factor;

        public static IWeightedDistribution<T> Distribution(
            Func<T, float> weightFunction,
            IWeightedDistribution<T> helper,
            float factor = 1) => new Rejection<T>(weightFunction, helper, factor);

        private Rejection(Func<T, float> weightFunction, IWeightedDistribution<T> helper, float factor)
        {
            _weightFunction = weightFunction;
            _helper = helper;
            _factor = factor;
        }

        public T Sample()
        {
            while (true)
            {
                T sample = _helper.Sample();
                float helperWeight = _helper.Weight(sample) * _factor;

                float weight = this.Weight(sample);

                if (Flip.Boolean(weight / helperWeight).Sample())
                    return sample;
            }
        }

        public float Weight(T item) => _weightFunction(item);
    }
}
