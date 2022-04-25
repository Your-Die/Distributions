﻿namespace Chinchillada.Distributions
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class FlipDistribution<T> : SerializableDistribution<T, IWeightedDistribution<T>>, IWeightedDistribution<T>
    {
        [SerializeField, Range(0, 1), OnValueChanged(nameof(UpdateDistribution))]
        private float probability = 0.5f;

        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private T head;

        [SerializeField, OnValueChanged(nameof(UpdateDistribution))]
        private T tail;

        public float Weight(T item) => this.Distribution.Weight(item);

        protected override IWeightedDistribution<T> BuildDistribution()
        {
            return Flip<T>.Distribution(this.head, this.tail, this.probability);
        }
    }
}