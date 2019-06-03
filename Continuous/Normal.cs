namespace Chinchillada.Distributions
{
    using UnityEngine;

    using SCU = StandardContinuousUniform;

    public class Normal : IWeightedDistribution<float>
    {
        private Normal(float mean, float standardDeviation)
        {
            this.Mean = mean;
            this.StandardDeviation = standardDeviation;
        }

        public float Mean { get; }

        public float StandardDeviation { get; }

        public static readonly Normal Standard = Distribution(0, 1);

        public static Normal Distribution(float mean, float standardDeviation) 
            => new Normal(mean, standardDeviation);

        public float Sample()
        {
            return this.Mean + this.StandardDeviation * StandardSample();
        }

        private static float StandardSample()
        {
            float sample1 = SCU.Distribution.Sample();
            float sample2 = SCU.Distribution.Sample();

            float log = Mathf.Log(sample1);
            float squareRoot = Mathf.Sqrt(-2.0f * log);
            float cos = Mathf.Cos(2.0f * Mathf.PI * sample2);

            return squareRoot * cos;
        }

        public float Weight(float variable)
        {
            float difference = variable - this.Mean;
            float exponent = Mathf.Exp(-difference * difference / (2 * StandardDeviation));

            return exponent * PiRoot / StandardDeviation;
        }

        private static readonly float PiRoot = 1 / Mathf.Sqrt(2 * Mathf.PI);
    }
}
