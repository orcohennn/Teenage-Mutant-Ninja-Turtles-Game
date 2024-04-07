using System.Collections;
using Main;
using StartEndScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RestartScreen
{
    /// <summary>
    /// Manages the restart screen functionality, including restarting or quitting the game.
    /// </summary>
    public class RestartScreenManager : MonoBehaviour
    {
        public FlickeringText continueText;
        public FlickeringText endText;
        public AudioSource playClickSound;
        public StarChooseManager starChooseManager;
        private bool _enterIsClicked;
        private void Start()
        {
            _enterIsClicked = false;
        }
    
        void Update()
        {
            if (Input.GetKey(KeyCode.Return) && !_enterIsClicked)
            {
                if (starChooseManager.GetChoose().Equals(Constants.Continue))
                {
                    continueText.StartBlinking();
                    _enterIsClicked = true;
                    playClickSound.Play();
                    StartCoroutine(RestartGame());
                }
                if (starChooseManager.GetChoose().Equals(Constants.End))
                {
                    endText.StartBlinking();
                    _enterIsClicked = true;
                    playClickSound.Play();
                    StartCoroutine(ExitGame());
                }
            
            }
        }

        /// <summary>
        /// Coroutine to restart the game.
        /// </summary>
        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(Constants.ChangeSceneTime);
            Destroy(GameManager.Instance.gameObject);
            SceneManager.LoadScene(Constants.OverWorldScene);
        }
    
        /// <summary>
        /// Coroutine to exit the game.
        /// </summary>
        private IEnumerator ExitGame()
        {
            yield return new WaitForSeconds(Constants.ChangeSceneTime);
            Application.Quit();
        }
    }
}
