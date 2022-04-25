using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Distributions
{
    public class FloatWeighted<T> : IWeightedDistribution<T>
    {
        private  readonly IDictionary<T, float> weights;
        private readonly float weightSum;

        public static IWeightedDistribution<T> Distribution(IDictionary<T, float> weights)
        {
            var weightArray = weights.ToArray();
            
            switch (weightArray.Length)
            {
                case 0:
                    return Empty<T>.Distribution();
                case 1:
                    var item = weights.Keys.First();
                    return Singleton<T>.Distribution(item);
                case 2:
                    var item1 = weights.First();
                    var item2 = weights.Last();

                    var probability = item1.Value / (item1.Value + item2.Value);
                    return Flip<T>.Distribution(item1.Key, item2.Key, probability);
                default:
                    return new FloatWeighted<T>(weights);
            }
        }
        
        public T Sample(IRNG random)
        {
            var randomValue = random.Range(0, this.weightSum);
            foreach (var itemWeightPair in this.weights)
            {
                randomValue -= itemWeightPair.Value;
                if (randomValue <= 0)
                    return itemWeightPair.Key;
            }

            return this.weights.Keys.Last();
        }

        public float Weight(T item) => this.weights[item];

        private FloatWeighted(IDictionary<T, float> weights)
        {
            this.weights = weights;
            this.weightSum = this.weights.Values.Sum();
        }
    }
}