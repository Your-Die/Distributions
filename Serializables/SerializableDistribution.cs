using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Distributions.Serializables
{
    [Serializable]
    public abstract class SerializableDistribution<T, TDistribution> : IDistribution<T>, ISerializationCallbackReceiver
        where TDistribution : IDistribution<T>
    {
        protected TDistribution Distribution { get; private set; }

        public T Sample(IRNG random) => this.Distribution.Sample(random);
        protected abstract TDistribution BuildDistribution();

        protected void UpdateDistribution() => this.Distribution = this.BuildDistribution();

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => this.UpdateDistribution();
    }
}