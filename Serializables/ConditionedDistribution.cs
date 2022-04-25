namespace Chinchillada.Distributions
{
    using Behavior;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    using Distributions;

    public class ConditionedDistribution<T> : DiscreteSerializableDistribution<T>
    {
        [OdinSerialize, Required] private IDiscreteDistribution<T> underlying;

        [OdinSerialize, Required] private ICondition<T> condition;

        protected override IDiscreteDistribution<T> BuildDistribution()
        {
            return Conditioned<T>.Distribution(this.underlying, this.condition.Validate);
        }
    }
}