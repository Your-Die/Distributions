namespace Serializables
{
    using System;
    using Chinchillada.Distributions;
    using Chinchillada.Distributions.Serializables;
    using UnityEngine;

    [Serializable]
    public class StandardDiscreteUniformDistribution : SerializableDiscreteDistribution<int>
    {
        [SerializeField] private int minimum;
        [SerializeField] private int maximum;

        protected override IDiscreteDistribution<int> BuildDistribution()
        {
            return StandardDiscreteUniform.Distribution(this.minimum, this.maximum);
        }
    }
}