using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Distributions
{
    public class RandomBag<T> : IWeightedDistribution<T>
    {
        private readonly IDictionary<T, float> collection;

        private float totalWeight = 0;

        public RandomBag()
        {
            this.collection = new Dictionary<T, float>();
        }

        public RandomBag(IDictionary<T, float> weightedCollection) => this.collection = weightedCollection;

        public RandomBag(IEnumerable<T> items, Func<T, float> weightFunction = null)
        {
            if (weightFunction == null)
                weightFunction = _ => 1;
            
            this.collection = items.ToDictionary(
                item => item,
                weightFunction);
        }

        public bool Any() => this.collection.Any();
        
        public void Add(T item, float weight)
        {
            this.collection.Add(item, weight);
            this.totalWeight += weight;
        }

        public void Remove(T item)
        {
            if (!this.collection.TryGetValue(item, out var weight)) 
                return;
            
            this.totalWeight -= weight;
            this.collection.Remove(item);
        }

        public T ChooseRandom()
        {
            var random = UnityEngine.Random.Range(0, this.totalWeight);
            foreach (var item in this.collection.Keys)
            {
                var weight = this.collection[item];
                random -= weight;
                
                if (random <= 0)
                    return item;
            }

            return this.collection.Keys.Last();
        }

        public T ExtractRandom()
        {
            var item = this.ChooseRandom();
            this.Remove(item);

            return item;
        }

        T IDistribution<T>.Sample() => this.ChooseRandom();

        public float Weight(T item)
        {
            return this.collection.TryGetValue(item, out var weight)
                ? weight
                : default;
        }
    }
}