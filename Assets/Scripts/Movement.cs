using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed, jumpForce;
    public float groundDist;

    private Vector2 moveInput;

    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isGrounded;

    public Rigidbody rb;
    public SpriteRenderer sr;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundLayer);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jumped");
            Vector3 newVelocity = rb.linearVelocity;
            newVelocity.y = jumpForce; // Set the upward velocity for the jump
            rb.linearVelocity = newVelocity;
        }

        // Handle movement
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

        // Preserve the y velocity while updating horizontal movement
        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);

        // Flip the sprite based on movement direction
        if (moveInput.x != 0 && moveInput.x < 0)
        {
            sr.flipX = true;
        }
        else if (moveInput.x != 0 && moveInput.x > 0)
        {
            sr.flipX = false;
        }
    }
}
