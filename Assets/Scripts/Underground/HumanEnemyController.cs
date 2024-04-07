using System;
using System.Collections;
using DG.Tweening;
using Main;
using UnityEngine;

namespace Underground
{
    /// <summary>
    /// Enum representing different actions for the Human Enemy.
    /// </summary>
    public enum HumanEnemyAction { Idle, Attack, Move }

    /// <summary>
    /// Controls the behavior of the human enemy character.
    /// </summary>
    public class HumanEnemyController : MonoBehaviour
    {
    
        public float moveSpeed = 2f; // Movement speed
        public Transform player; // Player's transform
        public GameObject starPrefab; // Reference to the star prefab
        public float throwForce = 5f; // Force to throw the stars
        private bool _inAction;
        private Collider2D _collider;
        private Animator _animator;
        private int _numOfLife;
        private Renderer _renderer;

        /// <summary>
        /// Initializes the enemy's components and variables.
        /// </summary>
        void Start()
        {
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            _inAction = false;
            _numOfLife = 3;
            _renderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Executes the enemy's actions based on certain conditions.
        /// </summary>
        void FixedUpdate()
        {
            if(!_inAction){ChooseEnemyAction();}
        }

        /// <summary>
        /// Chooses the action for the enemy based on certain conditions.
        /// </summary>
        private void ChooseEnemyAction()
        {
            _inAction = true;
            HumanEnemyAction action = ChooseAction();
            switch (action)
            {
                case HumanEnemyAction.Idle:
                    StartCoroutine(IdleAction());
                    break;
                case HumanEnemyAction.Attack:
                    StartCoroutine(AttackAction());
                    break;
                case HumanEnemyAction.Move:
                    StartCoroutine(MoveTowardsPlayer());
                    break;
            }
        }

        /// <summary>
        /// Chooses the action for the enemy based on the player's position.
        /// </summary>
        private HumanEnemyAction ChooseAction()
        {
            HumanEnemyAction action;
            if (Math.Abs(transform.position.x - player.position.x) > 4.5f) 
            { action = HumanEnemyAction.Move; }
            else if (Math.Abs(transform.position.x - player.position.x) > 5f)
            { action = HumanEnemyAction.Idle; }
            else { action = HumanEnemyAction.Attack; }
            return action;
        }

        /// <summary>
        /// Executes the idle action for the enemy.
        /// </summary>
        private IEnumerator IdleAction()
        {
            yield return new WaitForSeconds(1f);
            _inAction = false;
        }
        
        /// <summary>
        /// Executes the attack action for the enemy.
        /// </summary>
        private IEnumerator AttackAction()
        {
            int starsToThrow = 3;
            while (starsToThrow > 0)
            {
                _animator.SetTrigger(Constants.ThrowBoolAnimation);
                GameObject star = Instantiate(starPrefab, transform.position, Quaternion.identity);
                Rigidbody2D starRigidBody2D = star.GetComponent<Rigidbody2D>();
                Vector2 throwDirection = new Vector2(-transform.localScale.x, 0);
                starRigidBody2D.velocity = throwDirection * throwForce;
                Destroy(star, 5f);
                yield return new WaitForSeconds(1f);
                starsToThrow -= 1;
            }
            _inAction = false;
        }
    
        /// <summary>
        /// Executes the movement action for the enemy towards the player.
        /// </summary>
        private IEnumerator MoveTowardsPlayer()
        {
            _animator.SetBool(Constants.MovingBoolAnimation, true);
            float duration = 2f; // Time to move towards player
            float startTime = Time.time;
            while (Time.time < startTime + duration)
            {
                // Calculate the direction towards the player
                Vector3 direction = player.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                if (direction.x < 0)
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
                        transform.localScale.y, transform.localScale.z);
                else if (direction.x > 0)
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 
                        transform.localScale.y, transform.localScale.z);

                // Move towards the player
                transform.position += direction * (moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait for a short delay
            _animator.SetBool(Constants.MovingBoolAnimation, false);
            yield return new WaitForSeconds(2f);
            _inAction = false;
        }

        /// <summary>
        /// Handles collision with other game objects.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.PlayerSwordTag)) { PlayerSwordCollision(); }
            else if (collision.gameObject.CompareTag(Constants.MonsterTag))
            { Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); }
            else if (collision.gameObject.CompareTag(Constants.PlayerTag)) { PlayerCollision(collision); }
        }
    
        /// <summary>
        /// Handles collision with the player character.
        /// </summary>
        private void PlayerCollision(Collision2D collision)
        {
            GameManager.Instance.DecreaseLife(5);
            Renderer collisionRenderer = collision.gameObject.GetComponent<Renderer>();
            collisionRenderer.material.
                DOColor(Color.red, 0.5f).OnComplete(() => ResetPlayerColor(collisionRenderer));
            Physics2D.IgnoreCollision(collision.collider, _collider);
            StartCoroutine(ReturnCollision(collision));
        }

        /// <summary>
        /// Returns collision between the enemy and the player.
        /// </summary>
        private IEnumerator ReturnCollision(Collision2D collision)
        {
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(collision.collider, _collider, false);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreCollision(_collider, collision.collider, false);
        }
    
        /// <summary>
        /// Handles collision with the player's sword.
        /// </summary>
        private void PlayerSwordCollision()
        {
            _numOfLife -= 1;
            _renderer.material.DOColor(Color.red, 0.5f).OnComplete(ResetColor);
            if (_numOfLife == 0)
            {
                _animator.SetBool(Constants.MonsterDieBoolAnimation, true);
                StartCoroutine(Die());
            }
            GameManager.Instance.IncreasePoints(Constants.HumanEnemyPoints);
        }

        /// <summary>
        /// Resets the color of the enemy.
        /// </summary>
        private void ResetColor() { _renderer.material.DOColor(Color.white, 0.5f); }
        
        /// <summary>
        /// Resets the color of the player.
        /// </summary>
        private void ResetPlayerColor(Renderer rend) { rend.material.DOColor(Color.white, 0.5f); }

        /// <summary>
        /// Destroys the enemy object when it dies.
        /// </summary>
        private IEnumerator Die()
        {
            yield return new WaitForSeconds(0.6f);
            Destroy(gameObject);
        }
    
    }
}