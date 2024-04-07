using System.Collections;
using DG.Tweening;
using Main;
using UnityEngine;

namespace Underground
{
    /// <summary>
    /// Controls the behavior of the little monster enemy.
    /// </summary>
    public class LittleMonster : MonoBehaviour
    {
        private float _moveSpeed;
        private Rigidbody2D _rb;
        private Animator _animator;
        private Collider2D _collider;
        private bool _die;

        /// <summary>
        /// Initializes the little monster's components and variables.
        /// </summary>
        void Start()
        {
            _moveSpeed = Constants.LittleMonsterMovementSpeed;
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            _die = false;
        }

        /// <summary>
        /// Updates the movement of the little monster.
        /// </summary>
        void FixedUpdate()
        {
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }

        /// <summary>
        /// Handles collision events with other game objects.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.WallTag)) { ChangeDirection(); }
            else if (collision.gameObject.CompareTag(Constants.PlayerSwordTag)) { PlayerSwordCollision(); }
            else if (collision.gameObject.CompareTag(Constants.MonsterTag))
            { Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); }
            else if (collision.gameObject.CompareTag(Constants.PlayerTag)) { PlayerCollision(collision); }
        }

        /// <summary>
        /// Handles collision with the player's sword.
        /// </summary>
        private void PlayerSwordCollision()
        {
            _animator.SetBool(Constants.MonsterDieBoolAnimation, true);
            StartCoroutine(Die());
            GameManager.Instance.IncreasePoints(Constants.LittleMonsterPoints);
        }

        /// <summary>
        /// Handles collision with the player character.
        /// </summary>
        private void PlayerCollision(Collision2D collision)
        {
            if (!_die)
            {
                GameManager.Instance.DecreaseLife(5);
                Renderer collisionRenderer = collision.gameObject.GetComponent<Renderer>();
                collisionRenderer.material.
                    DOColor(Color.red, 0.5f).OnComplete(() => ResetColor(collisionRenderer));
                Physics2D.IgnoreCollision(collision.collider, _collider, true);
                StartCoroutine(ReturnCollision(collision));
            }
        }
    
        /// <summary>
        /// Resets the color of the collided object.
        /// </summary>
        private void ResetColor(Renderer rend) { rend.material.DOColor(Color.white, 0.5f); }
    
        /// <summary>
        /// Delays for a short duration before re-enabling collision.
        /// </summary>
        private IEnumerator ReturnCollision(Collision2D collision)
        {
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(collision.collider, _collider, false);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreCollision(_collider, collision.collider, false);
        }

        /// <summary>
        /// Destroys the little monster object when it dies.
        /// </summary>
        private IEnumerator Die()
        {
            _die = true;
            yield return new WaitForSeconds(0.6f);
            Destroy(gameObject);
        }

        /// <summary>
        /// Changes the movement direction of the little monster.
        /// </summary>
        private void ChangeDirection()
        {
            var transform1 = transform;
            Vector3 newRotation = transform1.eulerAngles;
            newRotation.y += 180f;
            transform1.eulerAngles = newRotation;
            _moveSpeed *= -1f;
        }
    }
}
