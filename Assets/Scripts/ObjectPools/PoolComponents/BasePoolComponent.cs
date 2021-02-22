using System;
using Unity.Mathematics;
using UnityEngine;

namespace Disco.ObjectPooling
{
    public abstract class BasePoolComponent : MonoBehaviour, IPoolCoomponent
    {
        [SerializeField] protected Transform _transform;

        #region Translocate
        public virtual void Translocate(float3 position, float2 direction)
        {
            //Verify
            bool2 isZero = (direction == float2.zero);
            if (isZero.x && isZero.y)
                throw new ArgumentException();

            _transform.position = position;
            float angle = -math.atan2(direction.x, direction.y);
            _transform.rotation = quaternion.AxisAngle(math.forward(), angle);
        }

        public virtual void Translocate(float3 position, quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;
        }
        #endregion

#if UNITY_EDITOR
        private void OnValidate()
        {
            _transform = GetComponent<Transform>();
        }
#endif
    }
}
