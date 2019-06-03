using UnityEditor;

namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;

    public sealed class Markov<T> : IDistribution<IEnumerable<T>>
    {
        private readonly IDistribution<T> _initial;

        private readonly Func<T, IDistribution<T>> _transition;

        public static Markov<T> Distribution(IDistribution<T> initial, Func<T, IDistribution<T>> transition)
        {
            return new Markov<T>(initial, transition);
        }

        private Markov(IDistribution<T> initial, Func<T, IDistribution<T>> transition)
        {
            _initial = initial;
            _transition = transition;
        }

        public IEnumerable<T> Sample()
        {
            var current = _initial;
            while (true)
            {
                if (current is Empty<T>)
                    break;

                T sample = current.Sample();
                yield return sample;

                current = _transition(sample);
            }
        }
    }
}
