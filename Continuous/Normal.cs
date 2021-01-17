namespace Chinchillada.Distributions
{
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    public class Normal : IWeightedDistribution<float>
    {
        private Normal(float mean, float standardDeviation)
        {
            this.Mean              = mean;
            this.StandardDeviation = standardDeviation;
        }

        public float Mean { get; }

        public float StandardDeviation { get; }

        public static readonly Normal Standard = Distribution(0, 1);

        public static Normal Distribution(float mean, float standardDeviation) => new Normal(mean, standardDeviation);

        public float Sample(IRNG random)
        {
            return this.Mean + this.StandardDeviation * StandardSample(random);
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
            var difference = variable - this.Mean;
            var exponent   = Mathf.Exp(-difference * difference / (2 * this.StandardDeviation));

            return exponent * PiRoot / this.StandardDeviation;
        }

        private static readonly float PiRoot = 1 / Mathf.Sqrt(2 * Mathf.PI);
    }
}