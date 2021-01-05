using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Disco
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private ProjectilePool _pool;
        [SerializeField] private Transform _player;
        [SerializeField] private float _radiuseOfVision = 7;
        [SerializeField] private float _shootDuration = 0.2f;

        private Transform _transform;
        private float _radiuseOfVisionSq;
        private bool _isSooting;


        private void Awake()
        {
            _radiuseOfVisionSq = _radiuseOfVision * _radiuseOfVision;

            _pool = FindObjectOfType<ProjectilePool>();
            _player = FindObjectOfType<PlayerMover>().GetComponent<Transform>();
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            float distanceSq = math.distancesq(_transform.position, _player.position);
            bool inRadiuse = distanceSq <= _radiuseOfVisionSq;
            if (inRadiuse && !_isSooting)
            {
                _isSooting = true;
                StartCoroutine(AIShoot());
            }
            else if (!inRadiuse)
                _isSooting = false;
        }

        private IEnumerator AIShoot()
        {
            while (_isSooting)
            {
                float3 direction3 = _player.position - _transform.position;
                float2 direction2 = math.normalize(new float2(direction3.x, direction3.y));
                _pool.InstantiateBullet((float3)_transform.position + math.normalize(direction3) * 1.5f, direction2);
                yield return new WaitForSeconds(_shootDuration);
            }
        }
    }
}
