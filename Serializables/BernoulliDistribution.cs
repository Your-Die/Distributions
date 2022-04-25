namespace Chinchillada.Distributions.Serializables
{
    using System;
    using UnityEngine;

    [Serializable]
    public class BernoulliDistribution : SerializableDiscreteDistribution<int>
    {
        [SerializeField, Min(0)] private int zeroes;
        [SerializeField, Min(0)] private int ones;
        
        protected override IDiscreteDistribution<int> BuildDistribution()
        {
            return Bernoulli.Distribution(this.zeroes, this.ones);
        }
    }
}