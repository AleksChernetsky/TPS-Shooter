using UnityEngine;

public class AnimationHandler
{
    [Header("Movement")]
    private Vector3 _lerpedInput = Vector3.zero;
    private float _velocityLerp = 10f;

    [Header("Jump")]
    private readonly string _jumpAnim = "Jump";
    private readonly string _jumpLayer = "JumpLayer";
    private int _jumpLayerIndex;

    private Animator _animator;
    private float _transitionSpeed = 15f;

    public AnimationHandler(Animator animator)
    {
        _animator = animator;
    }

    public void Initialize()
    {
        _jumpLayerIndex = _animator.GetLayerIndex(_jumpLayer);
    }

    public void MovementAnim(Vector2 movementInput)
    {
        _lerpedInput = movementInput == Vector2.zero && _lerpedInput.magnitude < 0.1f
            ? Vector2.zero
            : Vector3.Lerp(_lerpedInput, movementInput, _velocityLerp * Time.deltaTime);

        _animator.SetFloat("Velocity", _lerpedInput.magnitude);
    }
    
    public void SetMotionType(Vector2 movementInput, bool isArmed, bool runInput, bool isGrounded)
    {
        // armed
        _animator.SetBool("Armed", isArmed);
        // run
        RunState(movementInput, runInput);
        // jump
        JumpState(isGrounded);
    }
    private void SetLayerWeight(bool condition, int layerIndex, float transitionSpeed)
    {
        float layerWeight = condition
                ? Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 1f, transitionSpeed * Time.deltaTime)
                : Mathf.Lerp(_animator.GetLayerWeight(layerIndex), 0f, transitionSpeed * Time.deltaTime);

        _animator.SetLayerWeight(layerIndex, layerWeight);
    }
    private void RunState(Vector2 inputValues, bool runInput)
    {
        if (inputValues != Vector2.zero)
            _animator.SetBool("Run", runInput);
        else
            _animator.SetBool("Run", false);
    }
    private void JumpState(bool isGrounded)
    {
        _animator.SetBool(_jumpAnim, !isGrounded);
        SetLayerWeight(!isGrounded, _jumpLayerIndex, _transitionSpeed);
    }
}
