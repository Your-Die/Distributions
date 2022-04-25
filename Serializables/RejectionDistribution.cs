namespace Serializables
{
    using Chinchillada.Distributions;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using Chinchillada;

    public class RejectionDistribution<T> : WeightedSerializableDistribution<T>
    {
        [OdinSerialize, Required] private IScorer<T> scorer;

        [OdinSerialize, Required] private IWeightedDistribution<T> helper;

        [OdinSerialize, Required] private float factor = 1;
        
        protected override IWeightedDistribution<T> BuildDistribution()
        {
            return Rejection<T>.Distribution(this.scorer.Evaluate, this.helper, this.factor);
        }
    }
}