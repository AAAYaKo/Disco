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

            _move.action.performed += x =>
            {
                _direction = x.ReadValue<Vector2>();
                _needMove = true;
            };

            _move.action.canceled += _ =>
            {
                _direction = float2.zero;
                _needMove = false;
            };

            _jump.action.performed += _ =>
            {
                if (!_isJumping)
                {
                    _isJumping = true;
                    float2 force = math.normalize(new float2(_direction.x, 1)) * _jumpForce;
                    _rigidbody.AddForce(force, ForceMode2D.Impulse);
                    _rigidbody.gravityScale = _customGravityScale;
                }
            };

            _jump.action.canceled += _ =>
            {
                _rigidbody.gravityScale = 1;
            };
        }

        private void OnEnable()
        {
            _move.action.Enable();
            _jump.action.Enable();
        }

        private void FixedUpdate()
        {
            if (_needMove && !_isJumping && !_isInAir)
                _rigidbody.MovePosition(_transform.position + Vector3.right * (_direction.x * _speed * Time.fixedDeltaTime));

            RaycastHit2D[] hits = new RaycastHit2D[1];
            Physics2D.Raycast(_transform.position, -Vector2.up, _contactFilter, hits);
            float distance = hits[0].distance;

            if (hits[0].distance >= _distanceToGround)
                _isInAir = true;
            else if (distance <= _distanceToGround && distance != 0)
                _isInAir = false;

            if (_isJumping)
            {
                if (_rigidbody.velocity.y <= 0)
                    _rigidbody.gravityScale = 1;

                if (!_isInAir)
                    _isJumping = false;
            }
        }
    }
}
