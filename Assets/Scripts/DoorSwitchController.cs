using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorSwitchController : MonoBehaviour
{
    [Header("Switch Settings")]
    [Tooltip("Checks whether Steve is close enough to use the switch")]
    [SerializeField] private bool switchActive;

    [Tooltip("The Sprite renderer used to change the switch colour")]
    [SerializeField] private SpriteRenderer switchSpriteRenderer;

    [Space(5)]

    [Header("Door Settings")]
    [Tooltip("The door that this switch will open and close")]
    [SerializeField] private GameObject door;

    [Tooltip("The collider used to block Steve when the door is closed")]
    [SerializeField] private BoxCollider2D doorCollider;

    [Tooltip("The Sprite Renderer used to change the door colour")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer;

    [Tooltip("Shows whether the door is currently open")]
    [SerializeField] private bool doorIsOpen;

    private void Start()
    {
        doorIsOpen = false;
        //Turns the switch to red to show the door is closed.
        switchSpriteRenderer.color = Color.red;
        //Sets the door to be brown.
        doorSpriteRenderer.color = Color.black;
        //Enables the door collider so Steve cannot pass through.
        doorCollider.enabled = true;
    }

    private void Update()
    {
        CheckDoorSwitch();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switchActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switchActive = false;
        }
    }

    private void CheckDoorSwitch()
    {
        if (switchActive && Keyboard.current.eKey.wasPressedThisFrame)
        {
            //Changes the door between open and closed
            doorIsOpen = !doorIsOpen;

            if (doorIsOpen)
            {
                switchSpriteRenderer.color = Color.green;
                doorSpriteRenderer.color = Color.white;
                doorCollider.enabled = false;
            }
            else 
            {
                switchSpriteRenderer.color = Color.red;
                doorSpriteRenderer.color = Color.black;
                doorCollider.enabled = true;
            }
        }
    }
}
