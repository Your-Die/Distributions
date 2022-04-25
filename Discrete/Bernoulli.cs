namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    /// <summary>
    /// Bernoulli distribution. Represents a binary distribution with weights for the values.
    /// </summary>
    public class Bernoulli : IDiscreteDistribution<int>
    {
        /// <summary>
        /// The amount of zeroes in this <see cref="IDiscreteDistribution{T}"/>.
        /// </summary>
        private readonly int zeroes;

        /// <summary>
        /// The amount of ones in this <see cref="IDiscreteDistribution{T}"/>.
        /// </summary>
        private readonly int ones;
        
        /// <summary>
        /// The chance to draw a zero.
        /// </summary>
        private float ZeroChance => (float) this.zeroes / (this.zeroes + this.ones);
        
        /// <summary>
        /// Construct a new instance of a <see cref="Bernoulli"/> distribution.
        /// </summary>
        /// <param name="zeroes">The amount of zeroes in the distribution.</param>
        /// <param name="ones">The amount of ones in the distribution.</param>
        private Bernoulli(int zeroes, int ones)
        {
            this.zeroes = zeroes;
            this.ones = ones;
        }

        /// <summary>
        /// Create a bernoulli distribution.
        /// </summary>
        /// <param name="zeroes">The amount of zeroes in the distribution.</param>
        /// <param name="ones">The amount of ones in the distribution.</param>
        /// <returns>A correct distribution.</returns>
        /// <exception cref="ArgumentException">Thrown when one of the chances is less than zero.</exception>
        public static IDiscreteDistribution<int> Distribution(int zeroes, int ones)
        {
            // Needs to be more or equal to zero.
            if (zeroes < 0 || ones < 0)
                throw new ArgumentException();
            // Empty distribution if both have no chance.
            if (zeroes == 0 && ones == 0)
                return Empty<int>.Distribution();
            
            // Singleton if only one has any chance.
            if (zeroes == 0)
                return Singleton<int>.Distribution(1);
            if (ones == 0)
                return Singleton<int>.Distribution(0);

            return new Bernoulli(zeroes, ones);
        }

        /// <inheritdoc />
        public int Sample(IRNG random) => SCU.Distribution.Sample(random) <= this.ZeroChance ? 0 : 1;

        /// <inheritdoc />
        public IEnumerable<int> Support() => Enumerable.Range(0, 2);

        /// <inheritdoc />
        public int Weight(int variable)
        {
            return variable switch
            {
                0 => this.zeroes,
                1 => this.ones,
                _ => 0
            };
        }

        /// <inheritdoc />
        float IWeightedDistribution<int>.Weight(int item) => this.Weight(item);
    }
}
