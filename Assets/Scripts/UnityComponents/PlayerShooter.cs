using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Disco
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerShooter : MonoBehaviour
    {
        private const float SAFE_DISTANCE = 1.5f;

        [SerializeField] private ProjectilePool _instantiator;
        [SerializeField] private InputActionReference _shoot;
        private Transform _transform;
        private PlayerMover _mover;
        private float2 shootDirection
        {
            get
            {
                bool2 isntZero = _mover.Direction != float2.zero;
                if (isntZero.x || isntZero.y)
                    _oldDircetion = math.normalize(_mover.Direction);
                return _oldDircetion;
            }
        }
        private float2 _oldDircetion = Vector2.right;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _mover = GetComponent<PlayerMover>();

            _instantiator = FindObjectOfType<ProjectilePool>();
            _shoot.action.performed += _ =>
            {
                _instantiator.InstantiateBullet((float3)_transform.position + new float3(shootDirection * SAFE_DISTANCE, 0), shootDirection);
            };
        }

        private void OnEnable()
        {
            _shoot.action.Enable();
        }
    }
}
