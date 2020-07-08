using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Distributions.Serializables
{
    [Serializable]
    public class FlipDistribution<T> : SerializableDistribution<T>
    {
        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private float probability = 0.5f;

        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private T head;

        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private T tail;

        protected override IDistribution<T> BuildDistribution()
        {
            return Flip<T>.Distribution(this.head, this.tail, this.probability);
        }
    }
}