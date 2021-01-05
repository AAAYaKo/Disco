using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Disco
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrephab;
        [SerializeField] private int _defaultCount = 20;

        private readonly Queue<GameObject> _bullets = new Queue<GameObject>();
        private Transform _transform;


        #region Singlethone
        private static ProjectilePool _instance;

        public static ProjectilePool Instance => _instance;

        void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Debug.LogWarning($"Singleton for {GetType().Name} already exists.");
                Destroy(gameObject);
            }

            _transform = GetComponent<Transform>();
        }
        #endregion

        private void OnEnable()
        {
            InstantiateStartBullets();
        }

        private void OnDisable()
        {
            foreach (var i in _bullets)
                Destroy(i);
        }

        private void InstantiateStartBullets()
        {
            for (int i = 0; i < _defaultCount; i++)
            {
                AddBullet(_bullets);
            }
        }

        private void AddBullet(Queue<GameObject> newQueue)
        {
            GameObject bullet = Instantiate(_bulletPrephab, _transform);
            bullet.GetComponent<Bullet>().Pool = this;
            newQueue.Enqueue(bullet);
            bullet.SetActive(false);
        }

        public void InstantiateBullet(float3 position, float2 direction)
        {
            if (_bullets.Count == 0)
                AddBullet(_bullets);
            GameObject bullet = _bullets.Dequeue();
            bullet.SetActive(true);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
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
