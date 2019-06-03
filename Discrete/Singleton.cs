using System.Collections.Generic;

namespace Chinchillada.Distributions
{
    public class Singleton<T> : IDiscreteDistribution<T>
    {
        private readonly T _value;

        public Singleton(T value)
        {
            this._value = value;
        }

        public static Singleton<T> Distribution(T value) => new Singleton<T>(value);

        public T Sample() => _value;

        public IEnumerable<T> Support()
        {
            yield return _value;
        }

        public int Weight(T variable)
        {
            return EqualityComparer<T>.Default.Equals(this._value, variable) ? 1 : 0;
        }

        float IWeightedDistribution<T>.Weight(T item)
        {
            return Weight(item);
        }
    }
}
