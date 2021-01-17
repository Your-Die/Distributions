namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;

    public sealed class Markov<T> : IDistribution<IEnumerable<T>>
    {
        private readonly IDistribution<T> initial;

        private readonly Func<T, IDistribution<T>> transition;

        public static Markov<T> Distribution(IDistribution<T> initial, Func<T, IDistribution<T>> transition)
        {
            return new Markov<T>(initial, transition);
        }

        private Markov(IDistribution<T> initial, Func<T, IDistribution<T>> transition)
        {
            this.initial    = initial;
            this.transition = transition;
        }

        public IEnumerable<T> Sample(IRNG random)
        {
            var current = this.initial;
            while (true)
            {
                if (current is Empty<T>)
                    break;

                var sample = current.Sample(random);
                yield return sample;

                current = this.transition(sample);
            }
        }
    }
}
