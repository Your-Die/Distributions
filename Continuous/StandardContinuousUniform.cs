namespace Chinchillada.Distributions
{
    using UnityEngine;
    using SCU = StandardContinuousUniform;
    
    public class StandardContinuousUniform : IWeightedDistribution<float>
    {
        public static readonly SCU Distribution = new SCU();

        private StandardContinuousUniform()
        {
        }

        public float Sample()
        {
            return Random.value;
        }

        public float Weight(float variable) => 0f <= variable && variable < 1f ? 1f : 0f;
    }
}
