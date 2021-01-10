using System;
using Chinchillada.Distributions;
using UnityEngine;
using Random = Chinchillada.Foundation.Random;

namespace Distributions.Components
{
    [Serializable]
    public class IntRange : IDistribution<int>
    {
        [SerializeField] private int minimum = 0;
        [SerializeField] private int maximum = 100;
        [SerializeField] private bool inclusive = false;
        public int Sample()
        {
            var max = this.inclusive ? this.maximum + 1 : this.maximum;
            return Random.Range(this.minimum, max);
        }
    }
}