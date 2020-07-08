using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Distributions.Serializables
{
    [Serializable]
    public class UniformDistribution<T> : SerializableDistribution<T>
    {
        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private ICollection<T> items= new List<T>();
        
        protected override IDistribution<T> BuildDistribution() => DiscreteUniform<T>.Distribution(this.items);
    }
}