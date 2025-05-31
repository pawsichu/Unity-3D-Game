using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; // Reference to the CharacterController component

    public float speed = 12f; // Speed of the player movement
    public float jumpHeight = 3f; // Height of the jump 
    public float gravity = -9.81f * 2; // Gravity applied to the player

    public Transform groundCheck; // Transform to check if the player is grounded
    public float groundDistance = 0.4f; // Distance to check for ground 
    public LayerMask groundMask; // Layer mask to identify ground layers

    Vector3 velocity; // Current velocity of the player

    bool isGrounded; // Flag to check if the player is grounded
    bool isMoving; // Flag to check if the player is moving

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if the player is grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset vertical velocity when grounded
        }

        float x = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
        float z = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrows)

        Vector3 move = transform.right * x + transform.forward * z; // Calculate movement direction based on input

        controller.Move(move * speed * Time.deltaTime); // Move the player based on input and speed

        if (Input.GetButtonDown("Jump") && isGrounded) // Check if the jump button is pressed and the player is grounded
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity based on jump height and gravity
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity to the vertical velocity

        controller.Move(velocity * Time.deltaTime); // Move the player based on the calculated velocity

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true; // Set isMoving to true if the player has moved and is grounded
        }
        else
        {
            isMoving = false; // Set isMoving to false if the player has not moved or is not grounded
        }

        lastPosition = gameObject.transform.position; // Update lastPosition to the current position of the player
    }
}
