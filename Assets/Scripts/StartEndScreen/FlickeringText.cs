using System.Collections;
using Main;
using TMPro;
using UnityEngine;

namespace StartEndScreen
{
    /// <summary>
    /// Controls the flickering effect for text elements.
    /// </summary>
    public class FlickeringText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
    
        /// <summary>
        /// Starts the blinking effect for the text.
        /// </summary>
        public void StartBlinking()
        {
            StopCoroutine("Blink");
            StartCoroutine("Blink");
        }
        
        /// <summary>
        /// Coroutine for the blinking effect.
        /// </summary>
        private IEnumerator Blink()
        {
            while (true)
            {
                // Switches between fully visible and invisible states of the text.
                switch (_text.color.a.ToString())
                {
                    case "0":
                        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
                        yield return new WaitForSeconds(Constants.BlinkTime);
                        break;
                    case "1":
                        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
                        yield return new WaitForSeconds(Constants.BlinkTime);
                        break;
                }
            }
        }
    }
}
