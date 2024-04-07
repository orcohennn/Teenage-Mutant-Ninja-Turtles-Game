using System.Collections;
using Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overworld
{
    /// <summary>
    /// Controls the player character's movement and interaction in the overworld.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private AudioSource _gameOverSound;
        [SerializeField] private AudioSource _streetsSound;
        private float _moveSpeed;
        private Rigidbody2D _rb;
        private Animator _animator;
        private float _moveHorizontal;
        private float _moveVertical;
        private bool _die;

        private void Start()
        {
            _moveSpeed = Constants.PlayerSpeed;
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            transform.position = GameManager.Instance.GetOverWorldPosition();
            _die = false;
        }

        private void FixedUpdate()
        {
            if (!_die)
            {
                _moveHorizontal = Mathf.Round(Input.GetAxis(Constants.HorizontalDirection));
                _moveVertical = Mathf.Round(Input.GetAxis(Constants.VerticalDirection));
                Vector2 movement = new Vector2(_moveHorizontal, _moveVertical).normalized;
                _rb.velocity = movement * _moveSpeed;
            }
        }

        private void Update()
        {
            UpdateAnimation();
        }

        /// <summary>
        /// Updates player animations based on movement direction.
        /// </summary>
        private void UpdateAnimation()
        {
            _animator.SetBool(Constants.MoveDownAnimationBool, _moveVertical < 0);
            _animator.SetBool(Constants.MoveUpAnimationBool, _moveVertical > 0);
            _animator.SetBool(Constants.MoveHorizontalAnimationBool, _moveHorizontal != 0);

            ChangeHorizontalRotation();

            if (_moveVertical > 0)
            {
                _animator.SetFloat(Constants.IdleBlendTree, 0);
            }
            else if (_moveVertical < 0)
            {
                _animator.SetFloat(Constants.IdleBlendTree, 1);
            }
            else if (_moveHorizontal != 0)
            {
                _animator.SetFloat(Constants.IdleBlendTree, 2);
            }
        
        }

        /// <summary>
        /// Changes the player's rotation based on horizontal movement direction.
        /// </summary>
        private void ChangeHorizontalRotation()
        {
            Quaternion currentRotation = transform.rotation;
            if (_moveHorizontal < 0)
            {
                Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, 
                    180f, currentRotation.eulerAngles.z);
                transform.rotation = newRotation;
            }
            else if (_moveHorizontal > 0)
            {
                Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, 
                    0f, currentRotation.eulerAngles.z);
                transform.rotation = newRotation;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.MonsterTag) && !_die)
            {
                _die = true;
                _rb.velocity = Vector2.zero;
                StartCoroutine(Die());
            }
        }

        /// <summary>
        /// Initiates the player death sequence.
        /// </summary>
        private IEnumerator Die()
        {
            _gameOverSound.Play();
            _streetsSound.Stop();
            _animator.SetBool(Constants.MonsterDieBoolAnimation, true);
            yield return new WaitForSeconds(Constants.DieDelayTime);
            SceneManager.LoadScene(Constants.GameOverScene);

        }
    }
}
