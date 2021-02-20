using Disco.ObjectPooling;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Disco.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        private const float SAFE_DISTANCE = 1.5f;

        [SerializeField] private InputActionReference _move;
        [SerializeField] private InputActionReference _shoot;
        private Transform _transform;
        private float2 _shootDirection;


        private void Awake()
        {
            _transform = transform;

            _move.action.performed += x =>
            {
                _shootDirection = x.ReadValue<Vector2>();
            };
            _shoot.action.performed += _ =>
            {
                BulletPool.Instance.Spawn((float3)_transform.position + math.float3(_shootDirection * SAFE_DISTANCE, 0), _shootDirection);
            };
        }

        private void OnEnable()
        {
            _move.action.Enable();
            _shoot.action.Enable();
        }        
        private void OnDisable()
        {
            _move.action.Disable();
            _shoot.action.Disable();
        }
    }
}
