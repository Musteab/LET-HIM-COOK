using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputscript : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;

    private GameInputActions actions;
    private void Awake()
    {
        actions = new GameInputActions();
        actions.player.Enable();

        actions.player.Interact.performed += Interact_performed;
        actions.player.InteractAlternate.performed += InteractAlternate_performed;
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
            OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = actions.player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized; 

        return inputVector;
    }
}
