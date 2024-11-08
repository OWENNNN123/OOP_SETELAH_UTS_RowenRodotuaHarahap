using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public float maxSpeed = 5f;             
    public float timeToFullSpeed = 1f;      
    public float timeToStop = 0.5f;         
    public float stopClamp = 0.1f;          

    private Rigidbody2D rb;
    private float moveVelocity;
    private float moveFriction;
    private float stopFriction;

    private float xMin, xMax, yMin, yMax;
    private float playerWidth, playerHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveVelocity = maxSpeed / timeToFullSpeed;
        moveFriction = moveVelocity / timeToStop;
        stopFriction = stopClamp;

        
        Camera cam = Camera.main;
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            playerWidth = collider.bounds.extents.x;
            playerHeight = collider.bounds.extents.y;
        }
        else
        {
            Debug.LogWarning("Player object is missing a Collider2D. Boundary clamping may not work correctly.");
            playerWidth = playerHeight = 0;
        }

        
        xMin = -screenBounds.x + playerWidth;
        xMax = screenBounds.x - playerWidth;
        yMin = -screenBounds.y + playerHeight;
        yMax = screenBounds.y - playerHeight;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }

    public void Move()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + moveDirection * moveVelocity * Time.deltaTime, maxSpeed);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, stopFriction * Time.deltaTime);
        }

        if (rb.velocity.magnitude < stopClamp)
        {
            rb.velocity = Vector2.zero;
        }

        
        Vector3 clampedPosition = rb.position + rb.velocity * Time.deltaTime;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);
        rb.position = clampedPosition;
    }
}
