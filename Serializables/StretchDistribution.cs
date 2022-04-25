namespace Serializables
{
    using System;
    using Chinchillada.Distributions;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class StretchDistribution : WeightedSerializableDistribution<float>
    {
        [OdinSerialize, Required] private IWeightedDistribution<float> distribution;

        [SerializeField] private float shift;
        [SerializeField] private float stretch = 0;
        [SerializeField] private float around  = 0;

        protected override IWeightedDistribution<float> BuildDistribution()
        {
            return Stretch.Distribution(this.distribution, this.shift, this.stretch, this.around);
        }
    }
}