
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchController : MonoBehaviour
{
    [Header("Switch Settings")]
    [Tooltip("Shows if Steve is standing near the switch")]
    [SerializeField] private bool switchActive;

    [Tooltip("The Sprite Renderer used to change the switch colour")]
    [SerializeField] private SpriteRenderer switchSpriteRenderer;

    [Space(5)]

    [Header("Radio Settings")]
    [Tooltip("Shows whether the radio is turned on or off")]
    [SerializeField] private bool switchIsOn;

    [Header("Radio Connection")]
    [Tooltip("The radio controlled by this switch")]
    [SerializeField] private RadioController radioController;

    private void Start()
    {
        switchSpriteRenderer.color = Color.red;
    }

    private void Update()
    {
        checkSwitchKeys();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            switchActive = true;
            Debug.Log("Steve is at the switch");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            switchActive = false;
            switchIsOn = false;
            switchSpriteRenderer.color = Color.red;
            radioController.StopPlayingSong();
            Debug.Log("Steve has left the switch");
        }
    }

    void checkSwitchKeys()
    {
        if (switchActive && Keyboard.current.eKey.wasPressedThisFrame)
        {
            //toggles the switch off and on.
            switchIsOn = !switchIsOn;
            //checks if the switch is on.
            if (switchIsOn)
            {
                switchSpriteRenderer.color = Color.green;
                radioController.PlayNextSong();
            }

            else
            {
                switchSpriteRenderer.color = Color.red;
                radioController.StopPlayingSong();
            }
        }
        if (switchActive && Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            radioController.TurnVolumeUp();
        }

        if (switchActive && Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            radioController.TurnVolumeDown();
        }
    }
}
