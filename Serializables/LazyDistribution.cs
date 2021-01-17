using Chinchillada.Distributions;

namespace Packages.Distributions.Components
{
    using Chinchillada;

    public abstract class LazyDistribution<T> : IDistribution<T>
    {
        private IDistribution<T> distribution;
        
        public T Sample(IRNG random)
        {
            if (this.distribution == null) 
                this.distribution = this.BuildDistribution();

            return this.distribution.Sample(random);
        }

        protected abstract IDistribution<T> BuildDistribution();
    }
}