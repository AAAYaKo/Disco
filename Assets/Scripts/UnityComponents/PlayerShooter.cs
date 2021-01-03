using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Disco
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private BulletsInstantiator _instantiator;
        [SerializeField] private InputActionReference Shoot;
        private Transform _transform;
        private PlayerMover _mover;
        private float2 shootDirection
        {
            get
            {
                bool2 isntZero = _mover.Direction != float2.zero;
                if (isntZero.x || isntZero.y)
                    _oldDircetion = _mover.Direction;
                return _oldDircetion;
            }

            set => shootDirection = value;
        }
        private float2 _oldDircetion = Vector2.right;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _mover = GetComponent<PlayerMover>();
            _instantiator.Initialize();

            Shoot.action.performed += _ =>
            {
                _instantiator.InstantiateBullet(_transform.position, shootDirection);
            };
        }

        private void OnEnable()
        {
            Shoot.action.Enable();
        }
    }
}
