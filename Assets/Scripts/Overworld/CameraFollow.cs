using Main;
using UnityEngine;

namespace Overworld
{
    /// <summary>
    /// Controls the camera to follow the ninja turtle character in the overworld.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        public Transform ninjaTurtle; // Reference to the ninja turtle character's transform.
        private float _smoothSpeed; // Smoothing speed for camera movement.
        private Vector3 _offset; // Offset between camera and target.

        /// <summary>
        /// Initializes the CameraFollow component by setting up the smoothing speed and camera offset.
        /// </summary>
        private void Start()
        {
            _smoothSpeed = Constants.CameraSmooth;
            _offset = new Vector3(0f, 0f, Constants.CameraZOffset);
        }

        /// <summary>
        /// LateUpdate is called after all Update functions have been called. 
        /// It's used to ensure that the camera follows the ninja turtle smoothly.
        /// </summary>
        private void LateUpdate()
        {
            if (ninjaTurtle != null)
            {
                // Calculates the desired position of the camera based on ninja turtle's position and offset.
                Vector3 desiredPosition = ninjaTurtle.position + _offset;
                // Clamps the camera position within predefined boundaries.
                float clampedX = Mathf.Clamp(desiredPosition.x, Constants.CameraMinX, 
                    Constants.CameraMaxX);
                float clampedY = Mathf.Clamp(desiredPosition.y, Constants.CameraMinY, 
                    Constants.CameraMaxY);
                Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

                // Smoothly moves the camera towards the desired position.
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
                    clampedPosition, _smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}