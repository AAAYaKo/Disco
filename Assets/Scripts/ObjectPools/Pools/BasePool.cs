using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Disco.ObjectPooling
{
    /// <summary>
    /// Base Pool of IPoolComponents
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BasePool<T> : BasePool where T : MonoBehaviour, IPoolCoomponent
    {
        [SerializeField] private GameObject _prephab;
        [SerializeField] private int _defaultCount = 20;

        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        private readonly Dictionary<GameObject, T> _components = new Dictionary<GameObject, T>();

        private Transform _transform;


        protected virtual void Awake()
        {
            _transform = transform;
        }

        protected virtual void OnEnable()
        {
            InstantiateStartPoolObjects();
        }

        protected virtual void OnDisable()
        {
            //Destroy pooled objects
            foreach (var i in _pool)
            {
                Destroy(i);
            }
        }

        /// <summary>
        /// Spawn by position and look direction
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Spawn by position and rotation
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Disable and store pool Object
        /// </summary>
        /// <param name="pooledGameObject"></param>
        public override void Despawn(GameObject pooledGameObject)
        {
            _pool.Enqueue(pooledGameObject);
            pooledGameObject.SetActive(false);
        }

        /// <summary>
        /// Instantiate default pool Objects
        /// </summary>
        private void InstantiateStartPoolObjects()
        {
            for (int i = 0; i < _defaultCount; i++)
            {
                AddToPool();
            }
        }

        /// <summary>
        /// Add new Bullet to Pool
        /// </summary>
        private void AddToPool()
        {
            GameObject gameObject = Instantiate(_prephab, _transform);
            T component = gameObject.GetComponent<T>();
            component.SetPool(this);
            _components.Add(gameObject, component);
            _pool.Enqueue(gameObject);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Get actual pooled game Object
        /// </summary>
        /// <returns></returns>
        private GameObject GetGameObject()
        {
            if (_pool.Count == 0)
                AddToPool();

            GameObject gameObject = _pool.Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }
    }

    public abstract class BasePool : MonoBehaviour
    {
        public abstract void Despawn(GameObject pooledGameObject);
    }
}
