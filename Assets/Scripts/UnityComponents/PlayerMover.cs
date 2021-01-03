using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Disco
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private InputActionReference Move;
        [SerializeField] private InputActionReference Jump;
        [SerializeField] private float _speed = 0;
        [SerializeField] private float _jumpForce = 0;
        [SerializeField] private float _distanceToGround = 0;
        [SerializeField] private float _customGravityScale = 0;
        [SerializeField] private ContactFilter2D _contactFilter;

        public float2 Direction { get => _direction; }

        private float2 _direction = Vector2.right;
        private bool _needMove = false;
        private bool _isJumping = false;
        private bool _isInAir = false;
        private Rigidbody2D _rigidbody;
        private Transform _transform;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();


            Move.action.performed += x =>
            {
                _direction = x.ReadValue<Vector2>();
                _needMove = true;
            };

            Move.action.canceled += _ =>
            {
                _direction = float2.zero;
                _needMove = false;
            };

            Jump.action.performed += _ =>
            {
                _isJumping = true;
                float2 force = math.normalize(new float2(_direction.x, 1)) * _jumpForce;
                _rigidbody.AddForce(force, ForceMode2D.Impulse);
                _rigidbody.gravityScale = _customGravityScale;
            };

            Jump.action.canceled += _ =>
            {
                _rigidbody.gravityScale = 1;
            };
        }

        private void OnEnable()
        {
            Move.action.Enable();
            Jump.action.Enable();
        }

        private void FixedUpdate()
        {
            if (_needMove && !_isJumping && !_isInAir)
                _rigidbody.MovePosition(_transform.position + Vector3.right * (_direction.x * _speed * Time.fixedDeltaTime));

            if (_isJumping)
            {
                if (_rigidbody.velocity.y <= 0)
                    _rigidbody.gravityScale = 1;

                RaycastHit2D[] hits = new RaycastHit2D[1];
                Physics2D.Raycast(_transform.position, -Vector2.up, _contactFilter, hits);
                float distance = hits[0].distance;
                if (distance <= _distanceToGround && distance != 0 && _isInAir)
                {
                    _isJumping = false;
                    _isInAir = false;
                }
                else if(hits[0].distance >= _distanceToGround)
                    _isInAir = true;
            }
        }
    }
}
