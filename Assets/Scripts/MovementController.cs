using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //Below i used Header. Tooltip, SerializeField and Space which i'll give brief explinations on what they do.
    //Headers - adds a Header or Text above the SerializeField text.
    //Tooltip - adds a descriptive text when you hover over the SerializeField text.
    //Space - Creates a space between the above and below serializeFields.

    [SerializeField] private Rigidbody2D theRB;

    [Header("Player Movement")]
    [Tooltip("How fast steve moves left and right")]
    [SerializeField] private float speed = 5f;

    [Space(10)]

    [Header("Player Jumping")]
    [Tooltip("How much upward force Steve gets when jumping")]
    [SerializeField] private float jumpForce = 200f;

    private void Update()
    {
        MoveLeftAndRight();
        Jump();
    }

    void MoveLeftAndRight()
    {
        //Moves right using the right arrow key or the d key.
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            //Moves Steve right using the speed value while keeping his vertical movement the same.
            theRB.linearVelocity = new Vector2(speed, theRB.linearVelocity.y);
        }

        //Moves left using the left arrow key or the a key.
        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            //Moves Steve left using the speed value while keeping his vertical movement the same.
            theRB.linearVelocity = new Vector2(-speed, theRB.linearVelocity.y);
        }

        else
        {
            //Stops Steve from moving left or right while still allowing him to jump and fall.
            theRB.linearVelocity = new Vector2(0f, theRB.linearVelocity.y);
        }
    }

    void Jump()
    {
        //jump using the up arrow or the w key.
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            //Applies an upwards force to Steve using the jumpForce value to make him jump.
            theRB.AddForce(Vector2.up * jumpForce);
        }
    }
}
