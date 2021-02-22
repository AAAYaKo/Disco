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
        private BulletPool _pool;
        private float2 _shootDirection = Vector2.right;


        private void Awake()
        {
            _transform = transform;
            _pool = FindObjectOfType<BulletPool>();
        }

        private void OnEnable()
        {
            //Enable input
            _move.action.Enable();
            _shoot.action.Enable();
            //Subscribe to Input events
            _move.action.performed += OnMove;
            _shoot.action.performed += OnShoot;
        }

        //Subscribe to Input events
        private void OnDisable()
        {
            //Disable input
            _move.action.Disable();
            _shoot.action.Disable();
            //Unsubscribe to Input events
            _move.action.performed -= OnMove;
            _shoot.action.performed -= OnShoot;
        }

        private void OnMove(InputAction.CallbackContext context) => _shootDirection = context.ReadValue<Vector2>();

        private void OnShoot(InputAction.CallbackContext obj)
        {
            float3 spavnPosition = (float3)_transform.position + math.float3(_shootDirection * SAFE_DISTANCE, 0);
            _pool.Spawn(spavnPosition, _shootDirection);
        }
    }
}
