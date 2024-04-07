using UnityEngine;

namespace Overworld
{
    /// <summary>
    /// Controls the behavior of enemies in the overworld.
    /// </summary>
    public class OverworldEnemy : MonoBehaviour
    {
        private float _moveSpeed = 2f;
        [SerializeField] private float minX;
        [SerializeField] private float maxX;

        private int _direction = 1; // 1 for right, -1 for left
        private SpriteRenderer _spriteRenderer; // Reference to the sprite renderer

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        }
        
        /// <summary>
        /// Updates the enemy's position and flips its sprite if it reaches the boundaries.
        /// </summary>
        void Update()
        {
            transform.Translate(Vector2.right * (_direction * _moveSpeed * Time.deltaTime));
            if (transform.position.x >= maxX || transform.position.x <= minX)
            {
                _direction *= -1;
                FlipSprite();
            }
        }
        
        /// <summary>
        /// Flips the sprite horizontally to change its facing direction.
        /// </summary>
        void FlipSprite()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}