using System;
using UnityEngine;

namespace Chinchillada.Distributions.Serializables
{
    [Serializable]
    public abstract class SerializableDistribution<T> : IDistribution<T>, ISerializationCallbackReceiver
    {
        protected IDistribution<T> Distribution { get; private set; }

        public T Sample() => this.Distribution.Sample();

        protected abstract IDistribution<T> BuildDistribution();
        
        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => this.UpdateDistribution();

        protected void UpdateDistribution() => this.Distribution = this.BuildDistribution();
    }
}