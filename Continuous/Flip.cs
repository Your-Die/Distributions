namespace Chinchillada.Distributions
{
    using SCU = StandardContinuousUniform;

    public class Flip<T> : IWeightedDistribution<T>
    {
        private readonly T head;
        private readonly T tail;
        private readonly float probability;

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
            this.head        = head;
            this.tail        = tail;
            this.probability = probability;
        }

        public T Sample(IRNG random) => SCU.Distribution.Sample(random) <= this.probability ? this.head : this.tail;

        public float Weight(T item)
        {
            if (item.Equals(this.head))
                return this.probability;

            if (item.Equals(this.tail))
                return 1 - this.probability;

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
