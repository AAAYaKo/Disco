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

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _mover = GetComponent<PlayerMover>();
            _instantiator.Initialize();

            Shoot.action.performed += _ =>
            {
                _instantiator.InstantiateBullet(_transform.position, _mover.Direction);
            };
        }

        private void OnEnable()
        {
            Shoot.action.Enable();
        }
    }
}
