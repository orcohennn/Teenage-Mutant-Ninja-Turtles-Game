using Main;
using UnityEngine;

namespace Underground
{
    /// <summary>
    /// Controls the camera to follow the ninja turtle character in the underground scene.
    /// </summary>
    public class CameraFollowUnderground : MonoBehaviour
    {
        public Transform ninjaTurtle;
        private float _smoothSpeed;
        private Vector3 _offset;

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
                Vector3 desiredPosition = ninjaTurtle.position + _offset;
                float clampedX = Mathf.Clamp(desiredPosition.x, Constants.UnderCameraMinX, 
                    Constants.UnderCameraMaxX);
                float clampedY = Mathf.Clamp(desiredPosition.y, Constants.UnderCameraMinY, 
                    Constants.UnderCameraMaxY);
                Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, _smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}