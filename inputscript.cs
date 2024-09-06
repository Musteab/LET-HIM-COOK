using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputscript : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBinding";

    public static inputscript Instance { get; private set; }


    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    public enum Binding
    {
        Move_Up,
        Move_Down, 
        Move_Left, 
        Move_Right,
        Interact,
        AltInteract,
        Pause,
    }

    private GameInputActions actions;
    private void Awake()
    {
        Instance = this;

        actions = new GameInputActions();
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        actions.player.Enable();

        actions.player.Interact.performed += Interact_performed;
        actions.player.InteractAlternate.performed += InteractAlternate_performed;
        actions.player.Pause.performed += Pause_performed;

        

    }

    private void OnDestroy()
    {

        actions.player.Interact.performed -= Interact_performed;
        actions.player.InteractAlternate.performed -= InteractAlternate_performed;
        actions.player.Pause.performed -= Pause_performed;

        actions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Move_Up:
                return actions.player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return actions.player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return actions.player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return actions.player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return actions.player.Interact.bindings[0].ToDisplayString();
            case Binding.AltInteract:
                return actions.player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return actions.player.Pause.bindings[0].ToDisplayString();

        }
    }

    public void RebindBinding(Binding binding, Action onRebound)
    {
        actions.player.Disable();


        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default :
            case Binding.Move_Up:
                inputAction = actions.player.Move;
                bindingIndex = 1;
            break;
            case Binding.Move_Down:
                inputAction = actions.player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = actions.player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = actions.player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = actions.player.Interact;
                bindingIndex = 0;
                break;
            case Binding.AltInteract:
                inputAction = actions.player.InteractAlternate;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                actions.player.Enable();
                onRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, actions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);

            })
            .Start();
    }

}
