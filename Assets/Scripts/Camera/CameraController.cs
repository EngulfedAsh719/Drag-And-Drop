using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [SerializeField] private float leftLimit = -10f; 
    [SerializeField] private float rightLimit = 10f; 
    [SerializeField] private float scrollSpeed = 5f;  

    private float targetPosition;   
    private float lastInputTime = 0f;

    private void Start()
    {
        targetPosition = transform.position.x; 
    }

    private void Update()
    {
        HandleInput();
        SmoothMoveCamera();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width * 0.1f)  
            {
                targetPosition = Mathf.Max(leftLimit, transform.position.x - scrollSpeed * Time.deltaTime);
                lastInputTime = Time.time;
            }
            else if (touch.position.x > Screen.width * 0.9f)  
            {
                targetPosition = Mathf.Min(rightLimit, transform.position.x + scrollSpeed * Time.deltaTime);
                lastInputTime = Time.time;
            }
        }
    }

    private void SmoothMoveCamera()
    {
        float smoothPosition = Mathf.Lerp(transform.position.x, targetPosition, Time.deltaTime * scrollSpeed);
        transform.position = new Vector3(smoothPosition, transform.position.y, transform.position.z);
    }
}
