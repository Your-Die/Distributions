using Chinchillada.Distributions;

namespace Packages.Distributions.Components
{
    public abstract class LazyDistribution<T> : IDistribution<T>
    {
        private IDistribution<T> distribution;
        
        public T Sample()
        {
            if (this.distribution == null) 
                this.distribution = this.BuildDistribution();

            return this.distribution.Sample();
        }

        protected abstract IDistribution<T> BuildDistribution();
    }
}