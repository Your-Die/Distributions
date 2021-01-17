using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Chinchillada.Distributions.Components
{
    public class WeightedDistributionComponent : SerializedMonoBehaviour, IWeightedDistribution<int>
    {
        [OdinSerialize] private Dictionary<int, int> itemsByWeight = new Dictionary<int, int>();

        private IWeightedDistribution<int> distribution;
        
        public int Sample(IRNG random) => this.distribution.Sample(random);

        public float Weight(int item) => this.distribution.Weight(item);

        private void Awake() => this.distribution = this.itemsByWeight.ToWeighted();
    }
}