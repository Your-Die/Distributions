namespace Chinchillada.Distributions
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class DistributionAsset<T> : SerializedScriptableObject, IDistribution<T>
    {
        [OdinSerialize, Required] private IDistribution<T> distribution;

        public T Sample(IRNG random) => this.distribution.Sample(random);
    }
}