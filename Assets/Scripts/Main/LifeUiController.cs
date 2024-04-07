using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    /// <summary>
    /// Controls the UI elements related to player life.
    /// </summary>
    public class LifeUiController : MonoBehaviour
    {
        [SerializeField] private Slider slider; // Reference to the slider representing player life.
        [SerializeField] private Image image; // Reference to the image associated with the slider.

        private void Update()
        {
            // Enable or disable the image based on whether the player's life is zero.
            image.enabled = slider.value != 0;
        }
    }
}