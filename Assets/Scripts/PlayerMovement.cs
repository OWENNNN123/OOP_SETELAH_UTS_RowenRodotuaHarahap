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

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

        
        moveVelocity = maxSpeed / timeToFullSpeed;
        moveFriction = moveVelocity / timeToStop;
        stopFriction = stopClamp;
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
    }
}
