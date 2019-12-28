namespace Chinchillada.Distributions
{
    using SCU = StandardContinuousUniform;

    public class Flip<T> : IWeightedDistribution<T>
    {
        private readonly T _head;
        private readonly T _tail;
        private readonly float _probability;

        public static IWeightedDistribution<T> Distribution(T head, T tail, float probability)
        {
            if (probability <= 0)
                return Singleton<T>.Distribution(tail);
            if (probability >= 1 || head.Equals(tail))
                return Singleton<T>.Distribution(head);

            return new Flip<T>(head, tail, probability);
        }

        private Flip(T head, T tail, float probability)
        {
            _head = head;
            _tail = tail;
            _probability = probability;
        }

        public T Sample() => SCU.Distribution.Sample() <= _probability ? _head : _tail;

        public float Weight(T item)
        {
            if (item.Equals(_head))
                return _probability;

            if (item.Equals(_tail))
                return 1 - _probability;

            return 0;
        }
    }

    public static class Flip
    {
        public static IWeightedDistribution<bool> Boolean(float probability)
        {
            return Flip<bool>.Distribution(true, false, probability);
        }

        public static IWeightedDistribution<int> Binary(float probability) => Flip<int>.Distribution(1, 0, probability);
    }
}
