using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 5f;
    public float groundDist = 0.2f;

    [Header("Mouse/Aim")]
    public float mouseSensitivity = 1f; // multiplier if you want to scale mouse-facing updates
    public bool lockCursor = true;

    private Vector2 moveInput;
    private Vector3 moveVector = Vector3.zero;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isGrounded;

    [Header("Components")]
    public Rigidbody rb;
    public SpriteRenderer sr;

    // Internal state
    private bool jumpRequested;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Input
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // Ground check (non-physics side)
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundLayer);
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }

        // Compute mouse aim point on horizontal plane (player Y)
        Camera cam = Camera.main;
        Vector3 aimDirection = transform.forward; // fallback: face current forward
        if (cam != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0f));
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 rawDir = hitPoint - transform.position;
                rawDir.y = 0f;
                if (rawDir.sqrMagnitude > 1e-6f)
                {
                    aimDirection = rawDir.normalized;
                }
            }
        }

        // Build movement vector so WASD moves relative to mouse aim:
        // forward (W) moves toward aimDirection, right (D) moves perpendicular to it.
        Vector3 aimRight = Quaternion.Euler(0f, 90f, 0f) * aimDirection;
        Vector3 desired = aimDirection * moveInput.y + aimRight * moveInput.x;

        // Preserve magnitude (so diagonal isn't faster)
        if (desired.sqrMagnitude > 1f)
        {
            desired = desired.normalized;
        }

        moveVector = desired * speed;

        // Sprite flipping (based on horizontal movement direction relative to world X)
        if (sr != null)
        {
            if (moveVector.x < -0.01f) sr.flipX = true;
            else if (moveVector.x > 0.01f) sr.flipX = false;
        }

        // Rotate the player to face the mouse aim direction (smooth via Lerp using mouseSensitivity)
        if (aimDirection.sqrMagnitude > 1e-6f)
        {
            Quaternion targetRot = Quaternion.LookRotation(aimDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Mathf.Clamp01(mouseSensitivity * Time.deltaTime));
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // Preserve current vertical velocity
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveVector.x;
        velocity.z = moveVector.z;

        if (jumpRequested && isGrounded)
        {
            velocity.y = jumpForce;
            jumpRequested = false;
        }

        rb.linearVelocity = velocity;
    }
}