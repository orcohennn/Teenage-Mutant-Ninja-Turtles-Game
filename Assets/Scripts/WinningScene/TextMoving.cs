using UnityEngine;

namespace WinningScene
{
    public class TextMoving : MonoBehaviour
    {
        /// <summary>
        /// Changes the text position, upward.
        /// </summary>
        void Update()
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector2(position.x, position.y + 0.2f);
            transform1.position = position;
        }
    }
}
