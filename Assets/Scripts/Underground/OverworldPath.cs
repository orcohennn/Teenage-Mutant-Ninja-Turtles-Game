using Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Underground
{
    /// <summary>
    /// Represents a path in the underground scene that leads to the overworld.
    /// </summary>
    public class UndergroundPath : MonoBehaviour
    {
        [SerializeField] private float finishPathX;
        [SerializeField] private float finishPathY;
        
        /// <summary>
        /// Called when a collision occurs with another 2D collider, if collide with player the position
        /// of overworld's ninja turtle changes according to the path and the Overworld scene loaded.
        /// </summary>
        /// <param name="other">The collider the object collided with.</param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Constants.PlayerTag))
            {
                GameManager.Instance.SetOverWorldPosition(new Vector2(finishPathX, finishPathY));
                SceneManager.LoadScene(Constants.OverWorldScene);
            }
        }
    }
}
