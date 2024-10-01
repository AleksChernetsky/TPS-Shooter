using UnityEngine;

public class Movement
{
    [Header("Movement")]
    private Transform _body;
    private Transform _orientation;
    private CameraHandler _mainCamera;
    private float _walkSpeed = 20f;
    private float _runSpeed = 40f;
    private float _lerpBodyRotation = 9f;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;

    [Header("Rigidbody")]
    private float _groundDrag = 6f;
    private float _airDrag = 1f;
    private Rigidbody _rigidBody;

    [Header("Jump")]
    private float _jumpForce = 5f;
    private float _distanceToGround = 0.05f;
    public bool IsGrounded { get; private set; }

    public Movement(Transform body, Transform orientation, CameraHandler mainCamera, Rigidbody rigidBody)
    {
        _body = body;
        _orientation = orientation;
        _mainCamera = mainCamera;
        _rigidBody = rigidBody;
    }

    public void Move(Vector2 movementInput, bool isRunning)
    {
        // set move direction
        _lookDirection = _mainCamera.CameraLookDirection(_body);
        _orientation.forward = _lookDirection.normalized;
        _moveDirection = _orientation.right * movementInput.x + _orientation.forward * movementInput.y;
        // rotate body
        _body.forward = Vector3.Slerp(_body.forward, _moveDirection.normalized, _lerpBodyRotation * Time.deltaTime);
        // move
        float speed = isRunning ? _runSpeed : _walkSpeed;
        if (IsGrounded)
            _rigidBody.AddForce(_moveDirection * speed, ForceMode.Acceleration);

        CheckGround();
        ApplyDrag();
    }
    public void Jump()
    {
        if (IsGrounded)
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    private void CheckGround() => IsGrounded = Physics.CheckSphere(_body.transform.position + Vector3.down * _distanceToGround, _distanceToGround);
    private void ApplyDrag() => _rigidBody.linearDamping = IsGrounded ? _groundDrag : _airDrag;
}