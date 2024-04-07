using System;
using System.Collections;
using Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartEndScreen
{
    /// <summary>
    /// Manages the start screen functionality, including transitioning to the next scene.
    /// </summary>
    public class StartScreen : MonoBehaviour
    {
        public FlickeringText startText;
        public AudioSource playClickSound;
        private bool _enterIsClicked;

        [SerializeField] private String nextScene;

        private void Start()
        {
            _enterIsClicked = false;
        }
    
        void Update()
        {
            if (Input.GetKey(KeyCode.Return) && !_enterIsClicked)
            {
                startText.StartBlinking();
                _enterIsClicked = true;
                playClickSound.Play();
                StartCoroutine(MoveScene());
            }
        }

        /// <summary>
        /// Coroutine to move to the next scene.
        /// </summary>
        private IEnumerator MoveScene()
        {
            yield return new WaitForSeconds(Constants.ChangeSceneTime);
            SceneManager.LoadScene(nextScene);
        }
    }
}
