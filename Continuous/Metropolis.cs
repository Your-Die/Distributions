namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;

    public class Metropolis<T> : IWeightedDistribution<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly Func<T, float> _target;

        public static Metropolis<T> Distribution(
            Func<T, float> target,
            IDistribution<T> initial,
            Func<T, IDistribution<T>> proposal)
        {
            var markov = Markov<T>.Distribution(initial, Transition);
            var chain = markov.Sample();
            return new Metropolis<T>(target, chain.GetEnumerator());

            IDistribution<T> Transition(T item)
            {
                T candidate = proposal(item).Sample();
                float probability = target(candidate) / target(candidate);
                return Flip<T>.Distribution(candidate, item, probability);
            }
        }

        private Metropolis(Func<T, float> target, IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
            _target = target;
        }
        
        public T Sample()
        {
            _enumerator.MoveNext();
            return _enumerator.Current;
        }

        public float Weight(T item) => _target(item);
    }
}
