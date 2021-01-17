using System;
using UnityEngine;

namespace Chinchillada.Distributions
{
    [Serializable]
    public class FloatDistribution : IDistribution<float>
    {
        [SerializeField] private float minimum = 0;

        [SerializeField] private float maximum = 1f;

        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        public float Sample(IRNG random)
        {
            var point      = random.Float();
            var curvePoint = this.curve.Evaluate(point);

            return Mathf.Lerp(this.minimum, this.maximum, curvePoint);
        }

    }
}