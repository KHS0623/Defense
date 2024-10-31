using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundController : MonoBehaviour
{
    public float dragSpeed = 0.1f; 
    private Vector2 previousTouchPosition;  
    private bool isDragging = false;       

    private void Update()
    {
        if (Input.touchCount > 0)  
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = touch.position - previousTouchPosition;
                if (delta.x < 0 && GameManager.instance.isRightMax == true)
                {
                    return;
                }
                else if (delta.x > 0 && GameManager.instance.isLeftMax == true)
                {
                    return;
                }
                else if ((delta.x > 0 || delta.x < 0) && GameManager.instance.isMoving == true)
                {
                    return;
                }
                float deltaX = delta.x * (dragSpeed + 0.5f) * Time.deltaTime;

                transform.position += new Vector3(deltaX, 0, 0);
                previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))  
        {
            previousTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging) 
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - previousTouchPosition;
            if (mouseDelta.x < 0 && GameManager.instance.isRightMax == true)
            {
                return;
            }
            else if (mouseDelta.x > 0 && GameManager.instance.isLeftMax == true)
            {
                return;
            }
            else if ((mouseDelta.x > 0 || mouseDelta.x < 0) && GameManager.instance.isMoving == true)
            {
                return;
            }
            float deltaX = mouseDelta.x * (dragSpeed + 0.5f) * Time.deltaTime;

            transform.position += new Vector3(deltaX, 0, 0);
            previousTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))  
        {
            isDragging = false;
        }
    }
}


