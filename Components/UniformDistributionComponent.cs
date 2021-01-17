using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Distributions.Components
{
    public class UniformDistributionComponent : MonoBehaviour, IDistribution<int>
    {
        [SerializeField] private List<int> items = new List<int>();

        private IDistribution<int> distribution;
        
        private void Awake() => this.distribution = DiscreteUniform<int>.Distribution(this.items);

        public int Sample(IRNG random) => this.distribution.Sample(random);
    }
}