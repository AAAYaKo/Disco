using Disco.ObjectPooling;
using Unity.Mathematics;
using UnityEngine;

namespace Disco
{
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : BasePoolComponent
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
                //Pool object
                _pool.Despawn(gameObject);
            }
            else
                //Tick time
                _timeOfLife -= Time.deltaTime;

            //Move bullet
            _transform.position +=_translation * (Time.deltaTime * _speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //try hit damagedle object
            if (collision.TryGetComponent(out IDamageble damagedle))
                damagedle.TryAplyDamagage(_damage);
            _pool.Despawn(gameObject);
        }
    }
}
