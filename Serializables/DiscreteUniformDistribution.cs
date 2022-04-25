using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Distributions
{
    using Sirenix.Serialization;

    [Serializable]
    public class DiscreteUniformDistribution<T> : DiscreteSerializableDistribution<T>
    {
        [OdinSerialize, OnValueChanged(nameof(UpdateDistribution))]
        private IReadOnlyList<T> items = new List<T>();
        
        protected override IDiscreteDistribution<T> BuildDistribution() => DiscreteUniform<T>.Distribution(this.items);
    }
}