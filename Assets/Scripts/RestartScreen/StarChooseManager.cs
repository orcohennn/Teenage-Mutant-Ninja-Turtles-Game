using System;
using Main;
using UnityEngine;

namespace RestartScreen
{
    /// <summary>
    /// Manages the selection of options (continue or end) in the restart screen.
    /// </summary>
    public class StarChooseManager : MonoBehaviour
    {
        private String _curChoose;

        private void Start()
        {
            _curChoose = Constants.Continue;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                var transform1 = transform;
                transform1.position = new Vector2(transform1.position.x,0.1f);
                _curChoose = Constants.Continue;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                var transform1 = transform;
                transform1.position = new Vector2(transform1.position.x, -0.6f);
                _curChoose = Constants.End;
            }
        }

        /// <summary>
        /// Gets the current chosen option.
        /// </summary>
        /// <returns>The current chosen option (continue or end).</returns>
        public String GetChoose() { return this._curChoose; }
    
    }
}
