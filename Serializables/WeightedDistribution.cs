namespace Chinchillada.Distributions
{
    using Sirenix.Serialization;
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    [Serializable]
    public class WeightedDistribution<T> : SerializableDistribution<T, IDiscreteDistribution<T>>,
                                           IDiscreteDistribution<T>
    {
        [OdinSerialize, OnValueChanged(nameof(UpdateDistribution))]
        private Dictionary<T, int> weightedItems = new Dictionary<T, int>();

        protected override IDiscreteDistribution<T> BuildDistribution() => this.weightedItems.ToWeighted();
        public IEnumerable<T> Support() => this.Distribution.Support();

        int IDiscreteDistribution<T>.Weight(T variable) => this.Distribution.Weight(variable);

        public float Weight(T item) => this.Distribution.Weight(item);
    }
}