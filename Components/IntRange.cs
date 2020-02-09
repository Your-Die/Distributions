using System;
using Chinchillada.Distributions;
using UnityEngine;
using Random = Chinchillada.Utilities.Random;

namespace Distributions.Components
{
    [Serializable]
    public class IntRange : IDistribution<int>
    {
        [SerializeField] private int minimum = 0;
        [SerializeField] private int maximum = 100;
        
        public int Sample() => Random.Range(this.minimum, this.maximum);
    }
}