using System;
using UnityEngine;

namespace Chinchillada.Distributions
{
    using Random = UnityEngine.Random;

    [Serializable]
    public class FloatDistribution : IDistribution<float>
    {
        [SerializeField] private float minimum = 0;

        [SerializeField] private float maximum = 1f;

        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        public float Sample()
        {
            var point      = Random.value;
            var curvePoint = this.curve.Evaluate(point);

            return Mathf.Lerp(this.minimum, this.maximum, curvePoint);
        }
    }
}