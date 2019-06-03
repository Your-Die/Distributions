namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public class WeightedInteger : IDiscreteDistribution<int>
    {
        private readonly IList<int> _weights;
        private readonly IDiscreteDistribution<int>[] _distributions;

        public static IDiscreteDistribution<int> discreteDistribution(params int[] weights)
        {
            return Distribution(weights.AsEnumerable());
        }

        public static IDiscreteDistribution<int> Distribution(IEnumerable<int> weightCollection)
        {
            var weights = MathHelper.ShrinkValues(weightCollection).ToList();

            // Ensure we have valid weights.
            if (weights.Any(x => x < 0))
                throw new ArgumentException();

            // If no weights above 0, empty distribution.
            if (!weights.Any(x => x > 0))
                return Empty<int>.Distribution();

            weights = weights.DropEndWhile(weight => weight == 0).ToList();

            if (weights.Count == 1)
                return Singleton<int>.Distribution(0);

            if (weights.GetIndexIfSingle(i => i != 0, out int index))
                return Singleton<int>.Distribution(index);

            if (weights.Count == 2)
            {
                int zeroes = weights[0];
                int ones = weights[1];
                return Bernoulli.Distribution(zeroes, ones);
            }

            return new WeightedInteger(weights);
        }

        private WeightedInteger(IEnumerable<int> weights)
        {
            _weights = weights.EnsureList();
            int sum = _weights.Sum();
            int count = _weights.Count;
            _distributions = new IDiscreteDistribution<int>[count];

            // Distribute the weights into 2 dictionaries: one for weights lower than the average, one for weights higher.
            var lows = new Dictionary<int, int>();
            var highs = new Dictionary<int, int>();
            for (int i = 0; i < count; i++)
            {
                int weight = _weights[i] * count;
                if (weight == sum)
                    _distributions[i] = Singleton<int>.Distribution(i);
                else if (weight < sum)
                    lows.Add(i, weight);
                else
                    highs.Add(i, weight);
            }

            // Redistribute the weights.
            while (lows.Any()) // There should always be a high when there is a low.
            {
                // Grab the front low and high.
                var low = lows.First();
                lows.Remove(low.Key);
                var high = highs.First();
                highs.Remove(high.Key);

                // Fill up the distribution for the lower weight with values from the higher weight.
                int lowNeeds = sum - low.Value;
                _distributions[low.Key] =
                    Bernoulli.Distribution(low.Value, lowNeeds)
                             .Select(i => i == 0 ? low.Key : high.Key);

                // Put the higher weight back with it's new count.
                int newHigh = high.Value - lowNeeds;
                if (newHigh == sum)
                    _distributions[high.Key] = Singleton<int>.Distribution(high.Key);
                else if (newHigh < sum)
                    lows[high.Key] = newHigh;
                else
                    highs[high.Key] = newHigh;
            }
        }

        public int Sample()
        {
            int index = _weights.IndexDistribution().Sample();
            var distribution = _distributions[index];
            return distribution.Sample();
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(0, _weights.Count).Where(i => _weights[i] != 0);
        }

        public int Weight(int variable)
        {
            return _weights.ElementAtOrDefault(variable);
        }

        float IWeightedDistribution<int>.Weight(int item)
        {
            return Weight(item);
        }
    }
}
