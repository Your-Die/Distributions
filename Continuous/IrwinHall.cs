namespace Chinchillada.Distributions
{
    using System;
    using System.Linq;
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    /// <summary>
    /// Distribution sampled by calculating the sum of <see cref="sampleCount"/> independent uniform variables. 
    /// </summary>
    [Serializable]
    public class IrwinHall : IDistribution<float>
    {
        [SerializeField] private int sampleCount;

        private IrwinHall(int sampleCount)
        {
            this.sampleCount = sampleCount;
        }

        /// <summary>
        /// The amount of individual <see cref="SCU"/> samples averaged to generate a <see cref="IrwinHall"/> sample.
        /// </summary>
        public static IrwinHall Distribution(int sumCount) => new IrwinHall(sumCount);

        public float Sample(IRNG random)
        {
            return SCU.Distribution.Samples(random).Take(this.sampleCount).Sum();
        }
    }
}