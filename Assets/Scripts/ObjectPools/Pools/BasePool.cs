using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Disco.ObjectPooling
{
    public abstract class BasePool<T> : MonoBehaviour where T : MonoBehaviour, IPoolCoomponent
    {
        [SerializeField] private GameObject _prephab;
        [SerializeField] private int _defaultCount = 20;

        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        private readonly Dictionary<GameObject, T> _components = new Dictionary<GameObject, T>();

        private Transform _transform;

        #region Singlethone
        private static BasePool<T> _instance;

        public static BasePool<T> Instance => _instance;

        protected virtual void Awake()
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

        protected virtual void OnEnable()
        {
            InstantiateStartPoolObjects();
        }

        protected virtual void OnDisable()
        {
            foreach (var i in _pool)
            {
                Destroy(i);
            }
        }

        public virtual T Spawn(float3 position, float2 direction)
        {
            GameObject gameObject = GetGameObject();

            if (_components.TryGetValue(gameObject, out var component))
            {
                component.Translocate(position, direction);
                return component;
            }
            else
                throw new ArgumentException();
        }

        public virtual T Spawn(float3 position, quaternion rotation)
        {
            GameObject gameObject = GetGameObject();

            if (_components.TryGetValue(gameObject, out var component))
            {
                component.Translocate(position, rotation);
                return component;
            }
            else
                throw new ArgumentException();
        }

        public void Despawn(GameObject pooledGameObject)
        {
            _pool.Enqueue(pooledGameObject);
            pooledGameObject.SetActive(false);
        }

        private GameObject GetGameObject()
        {
            if (_pool.Count == 0)
                AddBullet();

            GameObject gameObject = _pool.Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }

        private void InstantiateStartPoolObjects()
        {
            for (int i = 0; i < _defaultCount; i++)
            {
                AddBullet();
            }
        }

        private void AddBullet()
        {
            GameObject gameObject = Instantiate(_prephab, _transform);
            T component = gameObject.GetComponent<T>();
            _components.Add(gameObject, component);
            _pool.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }
}
