using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Disco
{
    [CreateAssetMenu(fileName = "Instantiator", menuName = "ScriptableObjects/BulletInstantiator")]
    public class BulletsInstantiator : ScriptableObject
    {
        private const int DEFAULT_COUNT = 16;

        [SerializeField] private GameObject _bulletPrephab;
        private Queue<GameObject> _bullets;

        public void Initialize(Queue<GameObject> bullets = null)
        {
            _bullets = bullets ?? InitializeStartBullets();
        }

        private Queue<GameObject> InitializeStartBullets()
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            for (int i = 0; i < DEFAULT_COUNT; i++)
            {
                AddBullet(newQueue);
            }
            return newQueue;
        }

        private void AddBullet(Queue<GameObject> newQueue)
        {
            GameObject bullet = Instantiate(_bulletPrephab);
            newQueue.Enqueue(bullet);
            bullet.SetActive(false);
        }

        public void InstantiateBullet(float3 position, float2 direction)
        {
            if (_bullets.Count == 0)
                AddBullet(_bullets);
            GameObject bullet = _bullets.Dequeue();
            bullet.SetActive(true);
            BulletComponent bulletComponent = bullet.GetComponent<BulletComponent>();
            bulletComponent.Transform.position = position;
            bulletComponent.Direction = direction;
        }

        public void DespawnBullet(GameObject bullet)
        {
            _bullets.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }
}
