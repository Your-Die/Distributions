namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;

    public class Metropolis<T> : IWeightedDistribution<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly Func<T, float> target;

        public static Metropolis<T> Distribution(
            Func<T, float> target,
            IDistribution<T> initial,
            Func<T, IDistribution<T>> proposal,
            IRNG random)
        {
            var markov = Markov<T>.Distribution(initial, Transition);
            var chain = markov.Sample(random);
            return new Metropolis<T>(target, chain.GetEnumerator());

            IDistribution<T> Transition(T item)
            {
                T candidate = proposal(item).Sample(random);
                float probability = target(candidate) / target(candidate);
                return Flip<T>.Distribution(candidate, item, probability);
            }
        }

        private Metropolis(Func<T, float> target, IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
            this.target     = target;
        }
        
        public T Sample(IRNG _)
        {
            this.enumerator.MoveNext();
            return this.enumerator.Current;
        }

        public float Weight(T item) => this.target(item);
    }
}
