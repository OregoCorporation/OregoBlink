using System;
using UnityEngine;

namespace OregoBlink.util.unity.input
{
    public sealed class OregoVerticalTapBehaviour : MonoBehaviour
    {
        public event Action OnLeftTap;

        public event Action OnRightTap;

        [SerializeField] private Camera screenCamera;

        [SerializeField] private float centerSize = 5;

        private void Update()
        {
            //Standalone:
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                this.HandleDistance(Input.mousePosition);
            }
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                this.HandleDistance(touch.position);
            }
#endif
        }

        private void HandleDistance(Vector2 screenPosition)
        {
            var mousePosition = this.screenCamera.ScreenToWorldPoint(screenPosition);
            var distanceX = (mousePosition - this.transform.position).x;
            if (Mathf.Abs(distanceX) > this.centerSize)
            {
                //Left:
                if (distanceX < 0)
                {
                    this.OnLeftTap?.Invoke();
                }

                //Right:
                if (distanceX > 0)
                {
                    this.OnRightTap?.Invoke();
                }
            }
        }
    }
}