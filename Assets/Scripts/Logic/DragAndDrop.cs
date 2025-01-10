using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private bool isOnShelf = false; 
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private float scaleSpeed = 10f; 

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;  
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;  
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
        
        if (isOnShelf)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(0, -0.1f);  
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDragging)
        {
            if (other.CompareTag("BackShelf"))
            {
                SmoothScaleTo(originalScale * 0.75f);
                isOnShelf = true; 
            }
            else if (other.CompareTag("FrontShelf"))
            {
                SmoothScaleTo(originalScale * 1.25f);
                isOnShelf = true;  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BackShelf") || other.CompareTag("FrontShelf"))
        {
            SmoothScaleTo(originalScale);
            isOnShelf = false; 
        }
    }

    private void SmoothScaleTo(Vector3 newScale)
    {
        targetScale = newScale;
    }
}
