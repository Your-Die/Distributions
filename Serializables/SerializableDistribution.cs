using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Distributions
{
    [Serializable]
    public abstract class SerializableDistribution<TContent, TDistribution>
        : IDistribution<TContent>, ISerializationCallbackReceiver
        where TDistribution : IDistribution<TContent>
    {
        protected TDistribution Distribution { get; private set; }

        public TContent Sample(IRNG random) => this.Distribution.Sample(random);
        protected abstract TDistribution BuildDistribution();

        protected void UpdateDistribution() => this.Distribution = this.BuildDistribution();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => this.UpdateDistribution();
    }

    [Serializable]
    public abstract class DiscreteSerializableDistribution<T> : SerializableDistribution<T, IDiscreteDistribution<T>>,
                                                                IDiscreteDistribution<T>
    {
        public IEnumerable<T> Support() => this.Distribution.Support();

        public int Weight(T variable) => this.Distribution.Weight(variable);

        float IWeightedDistribution<T>.Weight(T item) => this.Distribution.Weight(item);
    }

    public abstract class WeightedSerializableDistribution<T> : SerializableDistribution<T, IWeightedDistribution<T>>,
                                                                IWeightedDistribution<T>
    {
        public float Weight(T item) => this.Distribution.Weight(item);
    }
}