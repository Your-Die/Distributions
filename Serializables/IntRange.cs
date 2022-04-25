namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;
    using Chinchillada;

    [Serializable]
    public class IntRange : IDiscreteDistribution<int>
    {
        [SerializeField] private int  minimum   = 0;
        [SerializeField] private int  maximum   = 100;
        [SerializeField] private bool inclusive = false;

        private int MaxValue => this.inclusive ? this.maximum : this.maximum - 1;

        public int Sample(IRNG random)
        {
            var max = this.inclusive ? this.maximum + 1 : this.maximum;
            return random.Range(this.minimum, max);
        }

        public IEnumerable<int> Support()
        {
            for (var value = this.minimum; value <= this.MaxValue; value++)
                yield return value;
        }

        public int Weight(int variable)
        {
            return this.Includes(variable) ? 1 : 0;
        }

        float IWeightedDistribution<int>.Weight(int item)
        {
            return this.Weight(item);
        }

        private bool Includes(int variable) => this.minimum <= variable && variable <= this.MaxValue;
    }
}