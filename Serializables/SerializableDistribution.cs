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

        public T Sample() => this.Distribution.Sample();

        protected abstract TDistribution BuildDistribution();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => this.UpdateDistribution();

        protected void UpdateDistribution() => this.Distribution = this.BuildDistribution();
    }
}