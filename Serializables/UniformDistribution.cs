using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Distributions.Serializables
{
    [Serializable]
    public class UniformDistribution<T> : SerializableDistribution<T, IDiscreteDistribution<T>>, IDiscreteDistribution<T>
    {
        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private ICollection<T> items= new List<T>();
        
        protected override IDiscreteDistribution<T> BuildDistribution() => DiscreteUniform<T>.Distribution(this.items);
        public IEnumerable<T> Support() => this.Distribution.Support();

        public int Weight(T variable) => this.Distribution.Weight(variable);

        float IWeightedDistribution<T>.Weight(T item) => this.Weight(item);
    }
}