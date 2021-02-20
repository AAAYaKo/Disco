using Disco.ObjectPooling;
using Unity.Mathematics;
using UnityEngine;

namespace Disco
{
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : BasePloolComponent
    {
        [SerializeField] private float _timeOfLife = 3;
        [SerializeField] private float _speed = 7;
        [SerializeField] private int _damage = 1;

        private Vector3 _translation;


        public override void Translocate(float3 position, float2 direction)
        {
            base.Translocate(position, direction);
            _translation = math.float3(direction, 0);
        }

        private void Update()
        {
            if (_timeOfLife <= 0)
            {
                _timeOfLife = 3;
                BulletPool.Instance.Despawn(gameObject);
            }
            else
                _timeOfLife -= Time.deltaTime;

            _transform.position +=_translation * (Time.deltaTime * _speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageble health))
                health.TryAplyDamagage(_damage);
            BulletPool.Instance.Despawn(gameObject);
        }
    }
}
