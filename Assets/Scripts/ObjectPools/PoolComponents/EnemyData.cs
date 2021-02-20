using System;
using Unity.Mathematics;
using UnityEngine;

namespace Disco.ObjectPooling
{
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class EnemyData : BasePloolComponent
    {
        public Transform Transform => _transform;
        public int ColliderId => _colliderId;
        public Rigidbody2D Rigidbody => _rigidbody;
        public GameObject GameObject => _gameObject;

        private int _colliderId;
        private Rigidbody2D _rigidbody;
        private GameObject _gameObject;


#if UNITY_EDITOR
        protected override void Awake()
        {
            base.Awake();
            _colliderId = GetComponent<Collider2D>().GetInstanceID();
            _rigidbody = GetComponent<Rigidbody2D>();
            _gameObject = gameObject;
        }
#endif

        public override void Translocate(float3 position, float2 direction)
        {
            if (direction.x == 0)
                throw new ArgumentException();

            _transform.position = position;
            float3 scale = _transform.localScale;
            scale.x = direction.x;
            _transform.localScale = scale;
        }
    }
}
