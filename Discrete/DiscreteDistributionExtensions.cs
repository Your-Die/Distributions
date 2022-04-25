﻿using System.Linq;

namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using Chinchillada;
    using SDU = StandardDiscreteUniform;

    public static class DiscreteDistributionExtensions
    {
        public static IDiscreteDistribution<TResult> Select<TSource, TResult>(
            this IDiscreteDistribution<TSource> distribution,
            Func<TSource, TResult> projection)
        {
            var dictionary = distribution.Support()
                .GroupBy(projection, distribution.Weight)
                .ToDictionary(group => group.Key, group => group.Sum());

            var keys = dictionary.Keys.EnsureList();
            var values = keys.Select(key => dictionary[key]);

            var weighted = WeightedInteger.Distribution(values.ToArray());
            return Projected<int, TResult>.Distribution(weighted, i => keys[i]);
        }

        public static IDiscreteDistribution<T> Where<T>(this IDiscreteDistribution<T> distribution,
            Func<T, bool> predicate)
        {
            var validSupport = distribution.Support().Where(predicate).EnsureList();
            var weights = validSupport.Select(distribution.Weight);

            return validSupport.ToWeighted(weights);
        }

        public static IDiscreteDistribution<TProjection> SelectMany<TPrior, TSample, TProjection>(
            this IDiscreteDistribution<TPrior> prior,
            Func<TPrior, IDiscreteDistribution<TSample>> likelihood,
            Func<TPrior, TSample, TProjection> projection)
        {
            var priorSupport = prior.Support().ToList();
            int lcm = priorSupport
                .Select(a => likelihood(a).TotalWeight())
                .Where(totalWeight => totalWeight != 0)
                .LCM();
            
            var weights = from s in priorSupport                      // Iterate over support
                          let weight = prior.Weight(s)                // get weight.
                          let probability = likelihood(s)             // Get inner distribution
                          let totalWeight = probability.TotalWeight() // Sum weight of inner distribution
                          from support in probability.Support()       // iterate inner distribution support
                          group weight * probability.Weight(support)  // "Combine Fractions" by multiplication
                                       * lcm / totalWeight
                              by projection(s, support);

            var dictionary = weights.ToDictionary(group => group.Key, group => group.Sum());
            return dictionary.Keys.ToWeighted(dictionary.Values);
        }

        public static IDiscreteDistribution<TResult> SelectMany<TPrior, TResult>(
            this IDiscreteDistribution<TPrior> distribution,
            Func<TPrior, IDiscreteDistribution<TResult>> likelihood)
        {
            return distribution.SelectMany(likelihood, (a, b) => b);
        }

        public static IDiscreteDistribution<(TPrior, TResult)> Joint<TPrior, TResult>(
            this IDiscreteDistribution<TPrior> distribution,
            Func<TPrior, IDiscreteDistribution<TResult>> likelihood)
        {
            return distribution.SelectMany(likelihood, (a, b) => (a,b));
        }

        public static Func<B, IDiscreteDistribution<C>> Posterior<A, B, C>(
            this IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<B>> likelihood,
            Func<A, B, C> projection)
        {
            return b => 
                from a in prior
                from otherB in likelihood(a)
                where Equals(b, otherB)
                select projection(a, b);
        }

        public static Func<B, IDiscreteDistribution<A>> Posterior<A, B>(
            this IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<B>> likelihood)
        {
            return prior.Posterior(likelihood, (a, b) => a);
        }
        
        public static IDiscreteDistribution<T> ToUniform<T>(this IEnumerable<T> items)
        {
            var list = items.EnsureList();

            var indices = list.IndexDistribution();
            return indices.Select(i => list[i]);
        }


        public static IDiscreteDistribution<T> ToWeighted<T>(this IDictionary<T, int> weightDictionary)
        {
            var items = weightDictionary.Keys.ToList();

            return items.ToWeighted(GetWeight);
            
            int GetWeight(T item) => weightDictionary[item];
        }
        
        public static IDiscreteDistribution<T> ToWeighted<T>(this IEnumerable<T> items, Func<T, int> weightFunction)
        {
            var itemList = items.ToList();
            var weights = itemList.Select(weightFunction);

            return itemList.ToWeighted(weights);
        }

        public static IDiscreteDistribution<T> ToWeighted<T>(this IEnumerable<T> items, IEnumerable<int> weights)
        {
            var list = items.EnsureList();
            var weightDistribution = WeightedInteger.Distribution(weights.ToArray());
            return weightDistribution.Select(i => list[i]);
        }

        public static IDiscreteDistribution<T> ToWeighted<T>(this IEnumerable<T> items, params int[] weights)
        {
            return items.ToWeighted(weights.AsEnumerable());
        }
        
        public static IDiscreteDistribution<T> ToWeighted<T>(this IEnumerable<(T, int)> weightedItems)
        {
            var items = new List<T>();
            var weights = new List<int>();

            // Split up the tuples.
            foreach (var (item, weight) in weightedItems)
            {
                items.Add(item);
                weights.Add(weight);
            }

            return items.ToWeighted(weights);
        }

        public static IDiscreteDistribution<int> IndexDistribution<T>(this IList<T> list)
        {
            return SDU.Distribution(0, list.Count - 1);
        }

        public static int TotalWeight<T>(this IDiscreteDistribution<T> distribution)
        {
            var support = distribution.Support();
            var weights = support.Select(distribution.Weight);

            return weights.Sum();
        }

        public static float ExpectedValue(this IDiscreteDistribution<int> distribution)
        {
            var support = distribution.Support();
            var weightedValues = support.Select(GetWeightedValue);
            float sum = weightedValues.Sum();
            int totalWeight = distribution.TotalWeight();

            return sum / totalWeight;

            float GetWeightedValue(int value)
            {
                int weight = distribution.Weight(value);
                return value * weight;
            }
        }

        public static float ExpectedValue(this IDiscreteDistribution<float> distribution)
        {

            var support = distribution.Support();
            var weightedValues = support.Select(GetWeightedValue);
            var sum = weightedValues.Sum();
            var totalWeight = distribution.TotalWeight();

            return sum / totalWeight;

            float GetWeightedValue(float value)
            {
                var weight = distribution.Weight(value);
                return value * weight;
            }
        }
    }
}
