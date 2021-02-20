using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Disco.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private InputActionReference _move;
        [SerializeField] private InputActionReference _jump;
        [SerializeField] private float _speed = 0;
        [SerializeField] private float _jumpForce = 0;
        [SerializeField] private float _distanceToGround = 0;
        [SerializeField] private float _customGravityScale = 0;
        [SerializeField] private ContactFilter2D _contactFilter;

        private float2 _direction = float2.zero;
        private bool _needMove = false;
        private bool _isJumping = false;
        private bool _isInAir = false;
        private Rigidbody2D _rigidbody;
        private Transform _transform;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        private void OnEnable()
        {
            //Enable input
            _move.action.Enable();
            _jump.action.Enable();
            //Subscribe to Input events
            _move.action.performed += OnMovePerformed;
            _move.action.canceled += OnMoveCanceled;
            _jump.action.performed += OnJumpPerformed;
            _jump.action.canceled += OnJumpCanceled;
        }

        private void OnDisable()
        {
            //Disable input
            _move.action.Disable();
            _jump.action.Disable();
            //Unsubscribe to Input events
            _move.action.performed -= OnMovePerformed;
            _move.action.canceled -= OnMoveCanceled;
            _jump.action.performed -= OnJumpPerformed;
            _jump.action.canceled -= OnJumpCanceled;
        }

        private void FixedUpdate()
        {
            if (_needMove && !_isJumping && !_isInAir)
                _rigidbody.MovePosition(_transform.position + Vector3.right * (_direction.x * _speed * Time.fixedDeltaTime));

            float distance = GetDistance();

            //Unlanding
            if (distance >= _distanceToGround)
                _isInAir = true;
            //Landing
            else if (distance != 0 && distance <= _distanceToGround)
                _isInAir = false;

            //Landing and changing of gravity scale
            if (_isJumping)
            {
                if (_rigidbody.velocity.y <= 0)
                    _rigidbody.gravityScale = 1;

                if (!_isInAir)
                    _isJumping = false;
            }
        }

        private float GetDistance()
        {
            var hits = new RaycastHit2D[1];
            Physics2D.Raycast(_transform.position, -Vector2.up, _contactFilter, hits);
            return hits[0].distance;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
            _needMove = true;
        }

        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            _direction = float2.zero;
            _needMove = false;
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            if (!_isJumping)
            {
                _isJumping = true;
                float2 force = math.normalize(new float2(_direction.x, 1)) * _jumpForce;
                _rigidbody.AddForce(force, ForceMode2D.Impulse);
                _rigidbody.gravityScale = _customGravityScale;
            }
        }

        private void OnJumpCanceled(InputAction.CallbackContext obj) => _rigidbody.gravityScale = 1;
    }
}
