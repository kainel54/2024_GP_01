using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controlls;
[CreateAssetMenu(menuName ="SO/InputSO")]
public class InputSO : ScriptableObject, IPlayerActions
{
    public Action OnJumpEvent;
    public Action<bool> OnFireEvent;
    public Action<bool> OnRunEvent;
    public Vector2 Movement { get; private set; }
    public Vector2 Delta { get; private set; }

    public Controlls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controlls();
        }
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Delta = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnFireEvent?.Invoke(true);
        }
        if (context.canceled)
        {
            OnFireEvent?.Invoke(false);
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRunEvent?.Invoke(true);
        }
        if (context.canceled)
        {
            OnRunEvent?.Invoke(false);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpEvent?.Invoke();
        }
    }
}
