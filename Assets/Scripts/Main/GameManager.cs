using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    /// <summary>
    /// Manages the game state, including player points, life, and position in the overworld.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the GameManager.
        /// </summary>
        public static GameManager Instance;

        private int _gamePoints; // Current points earned in the game.
        private int _playerLife; // Current life of the player.
        private Vector2 _overWorldPosition = Vector2.zero; // Current position of the player in the overworld.

        private void Awake()
        {
            // Sets the initial position in the overworld.
            _overWorldPosition = new Vector2(Constants.StartGamePosX, Constants.StartGamePosY);

            // Ensures only one instance of the GameManager exists throughout the game.
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Initializes the player's life to the maximum value defined in the Constants class.
            _playerLife = Constants.MaxPlayerLife;
        }

        /// <summary>
        /// Increases the game points by the specified amount.
        /// </summary>
        /// <param name="num">The amount of points to increase.</param>
        public void IncreasePoints(int num)
        {
            _gamePoints += num;
        }

        /// <summary>
        /// Retrieves the current game points.
        /// </summary>
        /// <returns>The current game points.</returns>
        public int GetPoints()
        {
            return _gamePoints;
        }

        /// <summary>
        /// Decreases the player's life by the specified amount.
        /// </summary>
        /// <param name="num">The amount to decrease the player's life.</param>
        public void DecreaseLife(int num)
        {
            _playerLife -= num;
            // Loads the game over scene if the player's life reaches zero.
            if (_playerLife <= 0)
            {
                SceneManager.LoadScene(Constants.GameOverScene);
            }
        }

        /// <summary>
        /// Retrieves the current player life.
        /// </summary>
        /// <returns>The current player life.</returns>
        public int GetLife()
        {
            return _playerLife;
        }

        /// <summary>
        /// Retrieves the current position of the player in the overworld.
        /// </summary>
        /// <returns>The current position of the player.</returns>
        public Vector2 GetOverWorldPosition()
        {
            return _overWorldPosition;
        }

        /// <summary>
        /// Sets the position of the player in the overworld.
        /// </summary>
        /// <param name="newPos">The new position to set.</param>
        public void SetOverWorldPosition(Vector2 newPos)
        {
            _overWorldPosition = newPos;
        }
    }
}
