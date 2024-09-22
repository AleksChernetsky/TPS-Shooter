using UnityEngine;

public class AnimationService : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 _lerpedInput = Vector3.zero;
    private float _velocityLerp = 6f;
    private readonly string _combatLayer = "CombatMotion";
    private int _combatLayerIndex;

    [Header("Aim")]
    private readonly string _aimLayer = "AimLayer";
    private int _aimLayerIndex;

    [Header("Shoot")]
    private readonly string _shootLayer = "ShootLayer";
    private int _shootLayerIndex;

    private float _transitionSpeed = 15f;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _combatLayerIndex = _animator.GetLayerIndex(_combatLayer);
        _aimLayerIndex = _animator.GetLayerIndex(_aimLayer);
        _shootLayerIndex = _animator.GetLayerIndex(_shootLayer);
    }
    public void MovementAnim(Vector2 inputValues, bool runInput, bool crouchInput, bool isArmed)
    {
        _lerpedInput = inputValues == Vector2.zero && _lerpedInput.magnitude < 0.1f
            ? Vector3.zero
            : Vector3.Lerp(_lerpedInput, inputValues, _velocityLerp * Time.deltaTime);

        SetMotionType(inputValues, runInput, crouchInput, isArmed);

        _animator.SetFloat("InputX", _lerpedInput.x);
        _animator.SetFloat("InputY", _lerpedInput.y);
        _animator.SetFloat("Velocity", _lerpedInput.magnitude);
    }
    private void SetMotionType(Vector2 inputValues, bool runInput, bool crouchInput, bool isArmed)
    {
        if (inputValues != Vector2.zero)
            _animator.SetBool("Runing", runInput);
        else
            _animator.SetBool("Runing", false);

        _animator.SetBool("Crouching", crouchInput);
        _animator.SetBool("Armed", isArmed);
    }
    public void MotionTypeLayer(bool isAiming) => SetLayerWeight(isAiming, _combatLayerIndex, _transitionSpeed);
    public void PlayAimAnim(bool isAiming) => SetLayerWeight(isAiming, _aimLayerIndex, _transitionSpeed);
    public void PlayShootAnim(bool isShooting) => SetLayerWeight(isShooting, _shootLayerIndex, _transitionSpeed);
    private void SetLayerWeight(bool input, int layerIndex, float transitionSpeed)
    {
        float layerWeight = input
                ? Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 1f, transitionSpeed * Time.deltaTime)
                : Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 0f, transitionSpeed * Time.deltaTime);

        _animator.SetLayerWeight(layerIndex, layerWeight);
    }
}
