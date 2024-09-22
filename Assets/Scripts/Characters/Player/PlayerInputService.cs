using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputService : MonoBehaviour, PlayerInput.IPlayerMotionMapActions
{
    public PlayerInput PlayerInput { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool AimInput { get; private set; }
    public bool ShootInput { get; private set; }
    public bool RunInput { get; private set; }
    public bool CrouchInput { get; private set; }
    public int WeaponIndex { get; private set; }

    public event Action OnInteractInput;
    public event Action OnHideWeaponInput; 
    public event Action<int> OnChooseWeapon;

    private void OnEnable()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();

        PlayerInput.PlayerMotionMap.Enable();
        PlayerInput.PlayerMotionMap.SetCallbacks(this);
    }
    private void OnDisable()
    {
        PlayerInput.PlayerMotionMap.Disable();
        PlayerInput.PlayerMotionMap.RemoveCallbacks(this);
    }
    public void OnMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();
    public void OnMouseLook(InputAction.CallbackContext context) { }
    public void OnAim(InputAction.CallbackContext context) => AimInput = context.performed;
    public void OnShoot(InputAction.CallbackContext context) => ShootInput = context.performed;
    public void OnRun(InputAction.CallbackContext context) => RunInput = context.performed;
    public void OnCrouch(InputAction.CallbackContext context) => CrouchInput = context.performed;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            OnInteractInput?.Invoke();
    }
    public void OnHideWeapon(InputAction.CallbackContext context)
    {
        if (context.started)
            OnHideWeaponInput?.Invoke();
    }
    public void OnTakeWeapon1(InputAction.CallbackContext context)
    {
        if (context.started)
            OnChooseWeapon?.Invoke(WeaponIndex = 1);
    }
    public void OnTakeWeapon2(InputAction.CallbackContext context)
    {
        if (context.started)
            OnChooseWeapon?.Invoke(WeaponIndex = 2);
    }
}
