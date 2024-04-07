using System;
using Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overworld
{
    /// <summary>
    /// Represents a path in the overworld that leads to an underground scene.
    /// </summary>
    public class OverworldPath : MonoBehaviour
    {
        public String undergroundPathScene;
        
        /// <summary>
        /// Called when a collision occurs with another 2D collider.
        /// </summary>
        /// <param name="other">The collider the object collided with.</param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Constants.PlayerTag))
            {
                SceneManager.LoadScene(undergroundPathScene);
            }
        }
    }
}
