using System.Linq;
using Chinchillada;

namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using Serializables;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    [Serializable]
    public sealed class MarkovDistribution<T> : SerializableDistribution<IEnumerable<T>, IDistribution<IEnumerable<T>>>
    {
        [OdinSerialize, Required] private IDistribution<T> initial;

        [OdinSerialize, Required]
        private Dictionary<T, IDistribution<T>> transitions = new Dictionary<T, IDistribution<T>>();

        protected override IDistribution<IEnumerable<T>> BuildDistribution()
        {
            return Markov<T>.Distribution(this.initial, Transition);

            IDistribution<T> Transition(T state)
            {
                return this.transitions.GetValueOrDefault(state, Empty<T>.Distribution());
            }
        }
    }
}