using System;
using Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Underground
{
    /// <summary>
    /// Manages the user interface elements in the underground scene.
    /// </summary>
    public class UiManager : MonoBehaviour
    {
        public TextMeshProUGUI pointsText;
        [SerializeField] private Slider[] sliders;
        private int _lastLife;
        
        /// <summary>
        /// Loads the life with maximum value.
        /// </summary>
        private void Start()
        {
            _lastLife = Constants.MaxPlayerLife;
        }
    
        /// <summary>
        /// Updates the points and life bar (Ui components).
        /// </summary>
        private void Update()
        {
            UpdatePoints();
            UpdateLifeBar();
        }
    
        /// <summary>
        /// Updates the points displayed on the UI.
        /// </summary>
        private void UpdatePoints()
        {
            int textlength = GameManager.Instance.GetPoints().ToString().Length;
            string zerosText = new string('0', Constants.NumOfUiZeros - textlength);
            pointsText.text = Constants.PointsTextPrefix + zerosText + GameManager.Instance.GetPoints();
        }
    
        /// <summary>
        /// Updates the player's life bar on the UI by adjusting
        /// the sliders according to the player's current life.
        /// </summary>
        private void UpdateLifeBar()
        {
            int playerLife = GameManager.Instance.GetLife();
            float lifeToErase = _lastLife - playerLife;
            foreach (Slider slider in sliders)
            {
                if (lifeToErase > 0)
                {
                    if (slider.value > 0)
                    {
                        float curRemove = Math.Min(lifeToErase, slider.value);
                        slider.value -= curRemove;
                        lifeToErase -= curRemove;
                    }
                    _lastLife = playerLife;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
