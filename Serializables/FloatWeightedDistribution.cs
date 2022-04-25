namespace Chinchillada.Distributions
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class FloatWeightedDistribution<T> : WeightedSerializableDistribution<T>
    {
        [OdinSerialize, Required] private Dictionary<T, float> weightedItems = new Dictionary<T, float>();

        protected override IWeightedDistribution<T> BuildDistribution()
        {
            return FloatWeighted<T>.Distribution(this.weightedItems);
        }
    }
}