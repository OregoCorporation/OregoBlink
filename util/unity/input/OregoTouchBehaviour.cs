using System;
using UnityEngine;

namespace OregoBlink.util.unity.input
{
    public sealed class OregoTouchBehaviour : MonoBehaviour
    {
        #region Const

        private const int SWIPE_MAGNITUDE = 125;

        #endregion

        #region Event

        public event Action OnTapEvent;

        public event Action OnDragStartedEvent;

        public event Action OnDragFinishedEvent;

        public event Action<float> OnHorizontalSwipeEvent;

        public event Action<float> OnVerticalSwipeEvent; 
        
        public event Action<float> OnSwipeLeft;

        public event Action<float> OnSwipeRight;

        public event Action<float> OnSwipeTop;

        public event Action<float> OnSwipeDown;

        #endregion

        private Vector2 startPosition;

        private void Update()
        {
            //Standalone:
#if UNITY_EDITOR
            var mousePosition = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                this.StartDrag(mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.FinishDrag(mousePosition);
            }

            //Device:
#elif UNITY_ANDROID || UNITY_IOS
            var touches = Input.touches;
            if (touches.Length > 0)
            {
                var touch = touches[0];
                var touchPosition = touch.position;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        this.StartDrag(touchPosition);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        this.FinishDrag(touchPosition);
                        break;
                }
            }
#endif
        }

        private void StartDrag(Vector2 inputStartPosition)
        {
            //Start:
            this.OnTapEvent?.Invoke();
            this.OnDragStartedEvent?.Invoke();
            this.startPosition = inputStartPosition;
        }

        private void FinishDrag(Vector2 endPosition)
        {
            //Calc delta:
            var swipeDelta = endPosition - this.startPosition;

            //Check swipe distance:
            var distance = swipeDelta.magnitude;
            if (distance > SWIPE_MAGNITUDE)
            {
                //Get x & y:
                var swipeByX = swipeDelta.x;
                var swipeByY = swipeDelta.y;

                //Horisontal swipe:
                var absSwipeByX = Mathf.Abs(swipeByX);
                var absSwipeByY = Mathf.Abs(swipeByY);
                if (absSwipeByX > absSwipeByY)
                {
                    //Left swipe:
                    if (swipeByX < 0)
                    {
                        this.OnSwipeLeft?.Invoke(absSwipeByX);
                    }
                    //Right swipe:
                    else
                    {
                        this.OnSwipeRight?.Invoke(absSwipeByX);
                    }

                    this.OnHorizontalSwipeEvent?.Invoke(swipeByX);
                }
                //Veritcal swipe:
                else
                {
                    //Top swipe:
                    if (swipeByY > 0)
                    {
                        this.OnSwipeTop?.Invoke(absSwipeByY);
                    }
                    //Down swipe:
                    else
                    {
                        this.OnSwipeDown?.Invoke(absSwipeByY);
                    }

                    this.OnVerticalSwipeEvent?.Invoke(swipeByY);    
                }
            }

            //Finish drag:
            this.OnDragFinishedEvent?.Invoke();
            this.startPosition = Vector2.zero;
        }
    }
}