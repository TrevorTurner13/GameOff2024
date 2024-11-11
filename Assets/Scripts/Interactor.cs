using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource; // The source of the interaction, typically the player
    private IInteractable interactableInRange; // Stores the interactable within range

    void Update()
    {
        // Check for interaction input
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (interactableInRange != null)
            {
                interactableInRange.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When entering the trigger, check if the collided object is interactable
        if (collision.TryGetComponent(out IInteractable interactOBJ))
        {
            interactableInRange = interactOBJ;
            Debug.Log("Interactable object in range.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Clear the interactable reference when exiting the trigger
        if (collision.TryGetComponent(out IInteractable interactOBJ) && interactOBJ == interactableInRange)
        {
            interactableInRange = null;
            Debug.Log("Interactable object out of range.");
        }
    }
}

