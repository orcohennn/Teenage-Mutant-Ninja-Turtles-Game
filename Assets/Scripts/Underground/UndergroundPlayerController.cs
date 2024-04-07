using DG.Tweening;
using Main;
using UnityEngine;

namespace Underground
{
    /// <summary>
    /// Controls the behavior and movement of the player character in the underground scene.
    /// </summary>
    public class UndergroundPlayerController : MonoBehaviour
    {
        [SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        private float _horizontal;
        private float _jumpPower;
        private bool _isFacingRight = true;
        private Rigidbody2D _rb;
        private Animator _animator;
        private Renderer _renderer;

        /// <summary>
        /// Init jump power and animator, renderer, rigid body variables.
        /// </summary>
        private void Start()
        {
            _jumpPower = Constants.PlayerJumpPower;
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Updates player's movement and control.
        /// </summary>
        private void Update()
        {
            _horizontal = Input.GetAxis(Constants.HorizontalDirection);
            if (Input.GetButtonDown(Constants.JumpButton) && IsGrounded())
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
                jumpSound.Play();
            }
            if (Input.GetButtonUp(Constants.JumpButton) && _rb.velocity.y > 0f)
            {
                var velocity = _rb.velocity;
                velocity = new Vector2(velocity.x, velocity.y * 0.5f);
                _rb.velocity = velocity;
            }
            Flip();
            Attack();
            UpdateAnimation();
        }

        /// <summary>
        /// Updates the animation parameters based on the player's state.
        /// </summary>
        private void UpdateAnimation()
        {
            _animator.SetBool(Constants.GroundedAnimBool,IsGrounded());
            _animator.SetBool(Constants.HorizontalAnimBool,_horizontal!= 0);
            _animator.SetBool(Constants.JumpingAnimBool,
                _rb.velocity.y > Constants.HighJumpAnimThreshold);
        
        }

        /// <summary>
        /// Handles attack input and plays attack sound.
        /// </summary>
        private void Attack()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                _animator.SetBool(Constants.AttackingAnimBool, true);
                attackSound.Play();
            }
            else
            {
                _animator.SetBool(Constants.AttackingAnimBool, false);
            }
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_horizontal * Constants.PlayerSpeed, _rb.velocity.y);
        }

        /// <summary>
        /// Checks if the player is grounded.
        /// </summary>
        /// <returns>True if the player is grounded, otherwise false.</returns>
        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        /// <summary>
        /// Flips the player character horizontally based on input direction.
        /// </summary>
        private void Flip()
        {
            if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
            {
                _isFacingRight = !_isFacingRight;
                var transform1 = transform;
                Vector3 localScale = transform1.localScale;
                localScale.x *= -1f;
                transform1.localScale = localScale;
            }
        }

        /// <summary>
        /// Handles collision with triggers, such as monsters, and performs necessary actions.
        /// It's for the stars that's the human enemy throws.
        /// </summary>
        /// <param name="other">The collider the player has collided with.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.MonsterTag))
            {
                GameManager.Instance.DecreaseLife(5);
                _renderer.material.DOColor(Color.red, 0.5f).OnComplete(ResetColor);
            }
        }

        /// <summary>
        /// Resets the player character's color back to normal after a certain duration.
        /// </summary>
        private void ResetColor()
        {
            _renderer.material.DOColor(Color.white, 0.5f);
        }
    }
}
