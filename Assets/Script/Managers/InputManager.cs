using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool touchStarted;
    private float minSwipeDistancePixels = 100f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStarted = true;
                    touchStartPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    if (touchStarted)
                    {
                        Swipe(touch);
                        touchStarted = false;
                    }
                    break;
                case TouchPhase.Canceled:
                    touchStarted = false;
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Moved:
                    break;
            }
        }
    }

    private void Swipe(Touch touch)
    {
        var lastPos = touch.position;
        var distance = Vector2.Distance(lastPos, touchStartPos);

        if (distance > minSwipeDistancePixels)
        {
            float dy = lastPos.y - touchStartPos.y;
            float dx = lastPos.x - touchStartPos.x;

            float angle = Mathf.Rad2Deg * Mathf.Atan2(dx, dy);

            angle = (360 + angle - 45) % 360;

            if (angle < 90)
            {
                // right
                GameUtil.SendGameEvent(eGameEventType.INPUT_RIGHT);
            }
            else if (angle < 180)
            {
                // down
                GameUtil.SendGameEvent(eGameEventType.INPUT_DOWN);
            }
            else if (angle < 270)
            {
                // left
                GameUtil.SendGameEvent(eGameEventType.INPUT_LEFT);
            }
            else
            {
                // up
                GameUtil.SendGameEvent(eGameEventType.INPUT_UP);
            }
        }
    }
}
