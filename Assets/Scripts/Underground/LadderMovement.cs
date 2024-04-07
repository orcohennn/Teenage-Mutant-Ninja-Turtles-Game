using Main;
using UnityEngine;

namespace Underground
{
    /// <summary>
    /// Controls the movement of the player character on ladders in the underground scene.
    /// </summary>
    public class LadderMovement : MonoBehaviour
    {
        private bool _isLadder;
        private bool _isClimbing;
        private Rigidbody2D _rb;
        private Animator _animator;
        private float _moveVertical;

        /// <summary>
        /// Initialized rigid body 2d and animator variables.
        /// </summary>
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// Updates the ladder movement behavior based on player input and collision with ladder triggers.
        /// </summary>
        private void Update()
        {
            if (_isLadder && Mathf.Abs(_moveVertical) > 0f)
            {
                _animator.SetBool(Constants.LadderIdleBoolAnimation, true);
                _animator.SetBool(Constants.LadderClimbBoolAnimation, true);
                _isClimbing = true;
            }
        
            if (Mathf.Abs(_moveVertical) == 0f && _isLadder)
            {
                _animator.SetBool(Constants.LadderClimbBoolAnimation, false);
            }
        }
    
        /// <summary>
        /// Handles the physics-based movement of the player while climbing a ladder.
        /// </summary>
        private void FixedUpdate()
        {
            if (_isLadder) { _moveVertical = Mathf.Round(Input.GetAxis(Constants.VerticalDirection)); }
            else { _moveVertical = 0f; }
            if (_isClimbing)
            {
                _rb.gravityScale = 0f;
                _rb.velocity = new Vector2(_rb.velocity.x, _moveVertical * Constants.PlayerSpeed);
            }
            else { _rb.gravityScale = 1f; }
        }

        /// <summary>
        /// Handles triggering of ladder-related behavior when the player enters a ladder trigger area.
        /// </summary>
        /// <param name="other">The collider the player enters.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.LadderTag)) { _isLadder = true; }
        }

        /// <summary>
        /// Resets ladder-related flags and animations when the player exits the ladder trigger area.
        /// </summary>
        /// <param name="other">The collider the player exits.</param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.LadderTag))
            {
                _isLadder = false;
                _isClimbing = false;
                _animator.SetBool(Constants.LadderIdleBoolAnimation, false);
                _animator.SetBool(Constants.LadderClimbBoolAnimation, false);
            }
        }
    }
}
