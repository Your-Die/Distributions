using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Distributions.Serializables
{
    [Serializable]
    public class WeightedDistribution<T> : SerializableDistribution<T>
    {
        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private Dictionary<T, int> weightedItems = new Dictionary<T,int>();
     
        protected override IDistribution<T> BuildDistribution() => this.weightedItems.ToWeighted();
    }
}