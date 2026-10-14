using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //Below i used Header. Tooltip, SerializeField and Space which i'll give brief explinations on what they do.
    //Headers - adds a Header or Text above the SerializeField text.
    //Tooltip - adds a descriptive text when you hover over the SerializeField text.
    //Space - Creates a space between the above and below serializeFields.

    [SerializeField] private Rigidbody2D theRB;

    [Header("Sprite Settings")]
    [Tooltip("Steve's Sprite Renderer used to flip his direction")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Player Movement")]
    [Tooltip("How fast steve moves left and right")]
    [SerializeField] private float speed = 4f;

    [Space(5)]

    [Tooltip("How fast Steve moves while holding Left Shift")]
    [SerializeField] private float sprintSpeed = 10f;

    [Space(5)]

    [Header("Player Jumping")]
    [Tooltip("How much upward force Steve gets when jumping")]
    [SerializeField] private float jumpForce = 6f;

    [Header("Ground Detection")]
    [Tooltip("Shows wheather Steve is currently on the ground")]
    [SerializeField] private bool isGrounded;

    [Header("Steve's Audio")]
    [Tooltip("The Audio Source that plays Steve's sounds")]
    [SerializeField] private AudioSource steveAudioSource;

    //Stores Steve's starting position in the level.
    private Vector3 startPosition;

    private int coinCount;

    private void Start()
    {
        //Saves Steve's starting position when the level starts.
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveLeftAndRight();
        Jump();
        CheckIfFallen();
    }

    void MoveLeftAndRight()
    {
        //doesnt contiue the method unless on the ground
        if (!isGrounded)
        { 
            return;
        }
        //Uses the normal movement speed by default.
        float currentSpeed = speed;

        //Steve runs when the left shift key is pressed.
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            //Changes current speed to the be the value of sprint speed.
            currentSpeed = sprintSpeed;
            //Debug.Log("Steve is now running");
        }
        
        //Moves right using the right arrow key or the d key.
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            //Moves Steve right using the speed value while keeping his vertical movement the same.
            theRB.linearVelocity = new Vector2(currentSpeed, theRB.linearVelocity.y);
            spriteRenderer.flipX = false;
            //Debug.Log("Steve is now facing right and is moving");
        }

        //Moves left using the left arrow key or the a key.
        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            //Moves Steve left using the speed value while keeping his vertical movement the same.
            theRB.linearVelocity = new Vector2(-currentSpeed, theRB.linearVelocity.y);
            spriteRenderer.flipX = true;
            //Debug.Log("Steve is now facing left and is moving");
        }
    }

    void Jump()
    {
        //jump using the up arrow or the w key and checks is your on the ground.
        if ((Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && isGrounded)
        {
            //Applies an upwards force to Steve using the jumpForce value to make him jump.
            theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            Debug.Log("Jump! Steve's Velocity is " + theRB.linearVelocity);
        }
    }

    void CheckIfFallen()
    {
        //Checks if Steve has fallen below the level.
        if (transform.position.y < -10f)
        {
            //returns Steve to his starting position.
            transform.position = startPosition;

            //stops Steve from moving when he spawn. 
            theRB.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                Debug.Log("Steve is on the ground!");
            }
            //Sets the grounded to true when Steve touches something.
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            steveAudioSource.Play();
            Debug.Log("Played mission failed");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isGrounded)
            {
                Debug.Log("Steve is off the ground!");
            }
            //Sets the grounded to false when Steve leaves the collider.
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected!");
            Destroy(collision.gameObject);
            coinCount++;
            Debug.Log("Total coins: " + coinCount);
        }

        else if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Steve hit a spike!");
            
            transform.position = startPosition;

            theRB.linearVelocity = Vector2.zero;
        }
    }
}
