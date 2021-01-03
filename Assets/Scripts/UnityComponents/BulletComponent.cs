using Unity.Mathematics;
using UnityEngine;

namespace Disco
{
    [ExecuteInEditMode]
    public class BulletComponent : MonoBehaviour
    {
        [SerializeField] private BulletsInstantiator _instantiator;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _timeOfLife = 3;
        [SerializeField] private float _bulletSpeed = 7;

        public float2 Direction
        {
            set
            {
                _translation = new float3(value.x, value.y, 0);
                float angle = -math.atan2(value.x, value.y);
                _transform.rotation = quaternion.AxisAngle(math.forward(), angle);
            }
        }

        public Transform Transform
        {
            get => _transform;
        }

        [SerializeField] private Vector3 _translation;


#if UNITY_EDITOR
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
#endif

        private void Update()
        {
            if (_timeOfLife <= 0)
            {
                _timeOfLife = 3;
                _instantiator.DespawnBullet(gameObject);
            }
            else
                _timeOfLife -= Time.deltaTime;

            _transform.position +=_translation * (Time.deltaTime * _bulletSpeed);
        }
    }
}
