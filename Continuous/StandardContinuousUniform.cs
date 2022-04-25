namespace Chinchillada.Distributions
{
    using System;
    using SCU = StandardContinuousUniform;
    
    [Serializable]
    public class StandardContinuousUniform : IWeightedDistribution<float>
    {
        public static readonly SCU Distribution = new SCU();

        private StandardContinuousUniform()
        {
        }

        public float Sample(IRNG random) => random.Float();

        public float Weight(float variable) => 0f <= variable && variable < 1f ? 1f : 0f;
    }
}
