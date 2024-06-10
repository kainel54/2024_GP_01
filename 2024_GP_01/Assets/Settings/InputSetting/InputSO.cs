using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controlls;
[CreateAssetMenu(menuName ="SO/InputSO")]
public class InputSO : ScriptableObject, IPlayerActions
{
    public Action<Vector2> OnMoveEvent;
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
        if (GameManager.Instance.IsEnd) return;
        Movement = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(Movement);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsEnd) return;

        Delta = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsEnd) return;

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
        if (GameManager.Instance.IsEnd) return;

        if (context.performed)
        {
            OnRunEvent?.Invoke(true);
            Debug.Log("True");
        }
        if (context.canceled)
        {
            OnRunEvent?.Invoke(false);
            Debug.Log("False");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsEnd) return;

        if (context.performed)
        {
            OnJumpEvent?.Invoke();
            Debug.Log("jump");
        }
    }
}
