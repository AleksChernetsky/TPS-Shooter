using UnityEngine;

public class AnimationHandler
{
    [Header("Movement")]
    private Vector3 _lerpedInput = Vector3.zero;
    private float _velocityLerp = 10f;

    [Header("Aim")]
    private readonly string _aimLayer = "AimLayer";
    private int _aimLayerIndex;

    [Header("Shoot")]
    private readonly string _shootLayer = "ShootLayer";
    private int _shootLayerIndex;

    private Animator _animator;
    private InputService _inputService;
    private WeaponHandler _weaponHandler;
    private float _transitionSpeed = 15f;

    public AnimationHandler(Animator animator, InputService inputService, WeaponHandler weaponHandler)
    {
        _animator = animator;
        _inputService = inputService;
        _weaponHandler = weaponHandler;
    }

    public void Initialize()
    {
        _aimLayerIndex = _animator.GetLayerIndex(_aimLayer);
        _shootLayerIndex = _animator.GetLayerIndex(_shootLayer);
    }
    public void UpdateAnimations()
    {
        MovementAnim(_inputService.MovementInput);
        SetMotionType(_inputService.MovementInput, _weaponHandler.IsArmed, _inputService.RunInput);
        CombatState(_weaponHandler.IsAiming, _weaponHandler.IsShooting);
    }
    private void MovementAnim(Vector2 movementInput)
    {
        _lerpedInput = movementInput == Vector2.zero && _lerpedInput.magnitude < 0.1f
            ? Vector2.zero
            : Vector3.Lerp(_lerpedInput, movementInput, _velocityLerp * Time.deltaTime);

        _animator.SetFloat("Velocity", _lerpedInput.magnitude);
    }
    private void SetMotionType(Vector2 movementInput, bool isArmed, bool runInput)
    {
        // armed
        _animator.SetBool("Armed", isArmed);
        // run
        RunState(movementInput, runInput);
    }
    private void CombatState(bool isAiming, bool isShooting)
    {
        SetLayerWeight(isAiming, _aimLayerIndex);
        SetLayerWeight(isShooting, _shootLayerIndex);
    }
    private void RunState(Vector2 inputValues, bool runInput)
    {
        if (inputValues != Vector2.zero)
            _animator.SetBool("Run", runInput);
        else
            _animator.SetBool("Run", false);
    }
    private void SetLayerWeight(bool condition, int layerIndex)
    {
        float layerWeight = condition
                ? Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 1f, _transitionSpeed * Time.deltaTime)
                : Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 0f, _transitionSpeed * Time.deltaTime);

        _animator.SetLayerWeight(layerIndex, layerWeight);
    }
}
