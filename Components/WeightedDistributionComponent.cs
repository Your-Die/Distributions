using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Chinchillada.Distributions.Components
{
    public class WeightedDistributionComponent : MonoBehaviour, IWeightedDistribution<int>
    {
        [OdinSerialize] private Dictionary<int, int> itemsByWeight = new Dictionary<int, int>();

        private IWeightedDistribution<int> distribution;
        
        public int Sample() => this.distribution.Sample();

        public float Weight(int item) => this.distribution.Weight(item);

        private void Awake() => this.distribution = this.itemsByWeight.ToWeighted();
    }
}