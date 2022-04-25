namespace Chinchillada.Distributions
{
    using System;
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    [Serializable]
    public class Normal : IWeightedDistribution<float>
    {
        [SerializeField] private float mean;

        [SerializeField] private float standardDeviation;

        private static readonly float PiRoot = 1 / Mathf.Sqrt(2 * Mathf.PI);

        public static readonly Normal Standard = Distribution(0, 1);

        private Normal(float mean, float standardDeviation)
        {
            this.mean              = mean;
            this.standardDeviation = standardDeviation;
        }

        public static Normal Distribution(float mean, float standardDeviation) => new Normal(mean, standardDeviation);

        public float Sample(IRNG random)
        {
            return this.mean + this.standardDeviation * StandardSample(random);
        }

        private static float StandardSample(IRNG random)
        {
            var sample1 = SCU.Distribution.Sample(random);
            var sample2 = SCU.Distribution.Sample(random);

            var log        = Mathf.Log(sample1);
            var squareRoot = Mathf.Sqrt(-2.0f * log);
            var cos        = Mathf.Cos(2.0f   * Mathf.PI * sample2);

            return squareRoot * cos;
        }

        public float Weight(float variable)
        {
            var difference = variable - this.mean;
            var exponent   = Mathf.Exp(-difference * difference / (2 * this.standardDeviation));

            return exponent * PiRoot / this.standardDeviation;
        }
    }
}