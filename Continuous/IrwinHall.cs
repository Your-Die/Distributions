using System.Linq;

namespace Chinchillada.Distributions
{
    using SCU = StandardContinuousUniform;

    /// <summary>
    /// Distribution sampled by calculating the sum of <see cref="SampleCount"/> independent uniform variables. 
    /// </summary>
    public class IrwinHall : IDistribution<float>
    {
        private IrwinHall(int sampleCount)
        {
            this.SampleCount = sampleCount;
        }

        /// <summary>
        /// The amount of individual <see cref="SCU"/> samples averaged to generate a <see cref="IrwinHall"/> sample.
        /// </summary>
        public int SampleCount { get; }

        public static IrwinHall Distribution(int sumCount) => new IrwinHall(sumCount);

        public float Sample()
        {
            return SCU.Distribution.Samples().Take(this.SampleCount).Sum();
        }
    }
}
    