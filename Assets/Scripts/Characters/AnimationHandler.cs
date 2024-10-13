using UnityEngine;

public class AnimationHandler
{
    [Header("Movement")]
    private Vector2 _lerpedInput = Vector2.zero;
    private float _velocityLerp = 10f;

    [Header("ArmedLayer")]
    private readonly string _aimedMovementLayer = "AimedMovementLayer";
    private int _aimedMovementLayerIndex;

    [Header("Aim")]
    private readonly string _aimLayer = "AimLayer";
    private int _aimLayerIndex;

    [Header("Shoot")]
    private readonly string _shootAnim = "Shoot";
    private readonly string _shootLayer = "ShootLayer";
    private int _shootLayerIndex;

    private float _transitionSpeed = 15f;

    private Animator _animator;
    private WeaponHandler _weaponHandler;

    public AnimationHandler(Animator animator, WeaponHandler weaponHandler)
    {
        _animator = animator;
        _weaponHandler = weaponHandler;
    }

    public void Initialize()
    {
        _aimLayerIndex = _animator.GetLayerIndex(_aimLayer);
        _shootLayerIndex = _animator.GetLayerIndex(_shootLayer);
        _aimedMovementLayerIndex = _animator.GetLayerIndex(_aimedMovementLayer);

        _weaponHandler.OnShootAction += PlayShootAnim;
    }
    public void UpdateAnimations(Vector2 movementInput, bool runInput)
    {
        MovementAnim(movementInput);
        SetMotionType(movementInput, runInput);
        SetCurrentLayer(runInput);
    }
    private void MovementAnim(Vector2 movementInput)
    {
        _lerpedInput = movementInput == Vector2.zero && _lerpedInput.magnitude < 0.1f
            ? Vector2.zero
            : Vector3.Lerp(_lerpedInput, movementInput, _velocityLerp * Time.deltaTime);

        _animator.SetFloat("Velocity", _lerpedInput.magnitude);
        _animator.SetFloat("InputX", _lerpedInput.x);
        _animator.SetFloat("InputY", _lerpedInput.y);
    }
    private void SetMotionType(Vector2 movementInput, bool runInput)
    {
        // armed
        _animator.SetBool("Armed", _weaponHandler.IsArmed);
        // run
        RunState(movementInput, runInput);
    }
    private void PlayShootAnim() => _animator.SetTrigger(_shootAnim);
    private void SetCurrentLayer(bool runInput)
    {
        SetLayerWeight(_aimedMovementLayerIndex, _weaponHandler.IsAiming || _weaponHandler.IsShooting);
        SetLayerWeight(_aimLayerIndex, !runInput && _weaponHandler.IsAiming);
        SetLayerWeight(_shootLayerIndex, _weaponHandler.IsShooting);
    }
    private void RunState(Vector2 movementInput, bool runInput)
    {
        if (movementInput != Vector2.zero)
            _animator.SetBool("Run", runInput);
        else
            _animator.SetBool("Run", false);
    }
    private void SetLayerWeight(int layerIndex, bool condition)
    {
        float layerWeight = condition
                ? Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 1f, _transitionSpeed * Time.deltaTime)
                : Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 0f, _transitionSpeed * Time.deltaTime);

        _animator.SetLayerWeight(layerIndex, layerWeight);
    }
}
