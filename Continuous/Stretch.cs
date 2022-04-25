using UnityEngine;

namespace Chinchillada.Distributions
{
    public sealed class Stretch : IWeightedDistribution<float>
    {
        private float shift;
        private float stretch;

        private IWeightedDistribution<float> distribution;

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
            this.distribution = distribution;
            this.shift    = shift;
            this.stretch  = stretch;
        }
        
        public float Sample(IRNG random)
        {
            var sample = this.distribution.Sample(random);
            return sample * this.stretch + this.shift;
        }

        public float Weight(float item)
        {
            var shifted   = item - this.shift;
            var stretched = shifted / this.stretch;
            var weight    = this.distribution.Weight(stretched);
            return weight / this.stretch;
        }
    }
}