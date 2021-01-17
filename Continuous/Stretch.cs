using UnityEngine;

namespace Chinchillada.Distributions
{
    public sealed class Stretch : IWeightedDistribution<float>
    {
        private IWeightedDistribution<float> _distribution;

        private float _shift;
        private float _stretch;

        public static IWeightedDistribution<float> Distribution(
            IWeightedDistribution<float> distribution,
            float shift,
            float stretch = 0f,
            float around = 0f)
        {
            if (Mathf.Approximately(stretch, 0) && 
                Mathf.Approximately(shift, 0))
                return distribution;

            shift = shift + around - around * stretch;
            return new Stretch(distribution, stretch, shift);
        }

        private Stretch(IWeightedDistribution<float> distribution, float stretch, float shift)
        {
            _distribution = distribution;
            _shift = shift;
            _stretch = stretch;
        }
        
        public float Sample(IRNG random)
        {
            var sample = _distribution.Sample(random);
            return sample * _stretch + _shift;
        }

        public float Weight(float item)
        {
            var shifted = item - _shift;
            var stretched = shifted / _stretch;
            var weight = this._distribution.Weight(stretched);
            return weight / this._stretch;
        }
    }
}