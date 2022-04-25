namespace Chinchillada.Distributions
{
    using System;
    using System.Linq;
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    /// <summary>
    /// Distribution sampled by calculating the mean of <see cref="sampleCount"/> independent uniform variables. 
    /// </summary>
    [Serializable]
    public class Bates : IDistribution<float>
    {
        /// <summary>
        /// The amount of individual <see cref="SCU"/> samples averaged to generate a <see cref="Bates"/> sample.
        /// </summary>
        [SerializeField] private int sampleCount;

        /// <summary>
        /// Construct a new <see cref="Bates"/> <see cref="Distribution"/>.
        /// </summary>
        /// <param name="sampleCount"></param>
        private Bates(int sampleCount)
        {
            this.sampleCount = sampleCount;
        }

        /// <summary>
        /// Get a new <see cref="Bates"/> <see cref="IDistribution{T}"/> that generates samples
        /// by calculating the mean of <see cref="sampleCount"/> random samples.
        /// </summary>
        /// <param name="sampleCount">The amount of individual <see cref="SCU"/> samples
        /// averaged to generate a <see cref="Bates"/> sample.</param>
        /// <returns>The <see cref="Bates"/> <see cref="IDistribution{T}"/>.</returns>
        public static Bates Distribution(int sampleCount) => new Bates(sampleCount);

        /// <inheritdoc cref="IDistribution{T}"/>
        public float Sample(IRNG random)
        {
            return SCU.Distribution.Samples(random).Take(this.sampleCount).Average();
        }
    }
}
