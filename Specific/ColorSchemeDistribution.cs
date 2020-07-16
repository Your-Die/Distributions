using System.Linq;
using Chinchillada.Distributions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Colors
{
    public class ColorSchemeDistribution : SerializedMonoBehaviour, IDistribution<int>, IDistribution<Color>, IColorschemeUser
    {
        [SerializeField] private IColorScheme colorScheme;

        private IDistribution<int> distribution;

        public IColorScheme ColorScheme
        {
            get => colorScheme;
            set
            {
                colorScheme = value;
                BuildDistribution();
            }
        }

        int IDistribution<int>.Sample()
        {
            return this.distribution.Sample();
        }

        Color IDistribution<Color>.Sample()
        {
            var index = this.distribution.Sample();
            return this.colorScheme[index];
        }
        
        private void BuildDistribution() => this.distribution = this.colorScheme.ToList().IndexDistribution();

        private void Start() => this.BuildDistribution();
    }
}