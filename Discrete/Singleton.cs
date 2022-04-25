
namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Singleton<T> : IDiscreteDistribution<T>
    {
        [SerializeField] private T value;

        public Singleton(T value) => this.value = value;

        public static Singleton<T> Distribution(T value) => new Singleton<T>(value);

        public T Sample(IRNG _) => this.value;

        public IEnumerable<T> Support()
        {
            yield return this.value;
        }

        public int Weight(T variable)
        {
            return EqualityComparer<T>.Default.Equals(this.value, variable) ? 1 : 0;
        }

        float IWeightedDistribution<T>.Weight(T item) => this.Weight(item);
    }
}
