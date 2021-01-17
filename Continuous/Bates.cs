namespace Chinchillada.Distributions
{
    using System.Linq;

    using SCU = StandardContinuousUniform;

    /// <summary>
    /// Distribution sampled by calculating the mean of <see cref="SampleCount"/> independent uniform variables. 
    /// </summary>
    public class Bates : IDistribution<float>
    {
        /// <summary>
        /// Construct a new <see cref="Bates"/> <see cref="Distribution"/>.
        /// </summary>
        /// <param name="sampleCount"></param>
        private Bates(int sampleCount)
        {
            this.SampleCount = sampleCount;
        }

        /// <summary>
        /// Get a new <see cref="Bates"/> <see cref="IDistribution{T}"/> that generates samples by calculating the mean of <see cref="SampleCount"/> random samples.
        /// </summary>
        /// <param name="sampleCount">The amount of individual <see cref="SCU"/> samples averaged to generate a <see cref="Bates"/> sample.</param>
        /// <returns>The <see cref="Bates"/> <see cref="IDistribution{T}"/>.</returns>
        public static Bates Distribution(int sampleCount) => new Bates(sampleCount);

        /// <summary>
        /// The amount of individual <see cref="SCU"/> samples averaged to generate a <see cref="Bates"/> sample.
        /// </summary>
        public int SampleCount { get; }

        /// <inheritdoc cref="IDistribution{T}"/>
        public float Sample(IRNG random)
        {
            return SCU.Distribution.Samples(random).Take(this.SampleCount).Average();
        }
    }
}
