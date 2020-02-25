using System.Linq;
using Chinchillada.Utilities;

namespace Chinchillada.Distributions
{
    using System.Collections.Generic;

    public sealed class MarkovBuilder<T>
    {
        private DistributionBuilder<T> _initial;

        private Dictionary<T, DistributionBuilder<T>> _transitions = new Dictionary<T, DistributionBuilder<T>>();

        public void AddInitial(T item)
        {
            _initial.Add(item);
        }

        public void AddTransition(T from, T to)
        {
            if (!_transitions.ContainsKey(from))
                _transitions[from] = new DistributionBuilder<T>();

            _transitions[from].Add(to);
        }

        public Markov<T> ToDistribution()
        {
            var initial = _initial.ToDistribution();
            var transitions = _transitions.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.ToDistribution());

            IDistribution<T> Transition(T state) => transitions.GetValueOrDefault(state, Empty<T>.Distribution());
            return Markov<T>.Distribution(initial, Transition);
        }
    }
}
