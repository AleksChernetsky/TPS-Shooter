using UnityEngine;

public class Movement
{
    [Header("Movement")]
    private float _walkSpeed = 20f;
    private float _runSpeed = 40f;
    private float _lerpBodyRotation = 9f;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;

    private Transform _body;
    private Transform _orientation;
    private CameraHandler _mainCamera;
    private Rigidbody _rigidBody;

    public Movement(Transform body, Transform orientation, CameraHandler mainCamera, Rigidbody rigidBody)
    {
        _body = body;
        _orientation = orientation;
        _mainCamera = mainCamera;
        _rigidBody = rigidBody;
    }

    public void UpdateMovement(Vector2 movementInput, bool isRunning, WeaponHandler weaponHandler)
    {
        Move(movementInput, isRunning, weaponHandler.IsAiming);
        SetBodyMotionType(weaponHandler.IsAiming, weaponHandler.IsShooting, weaponHandler.IsArmed);
    }
    private void Move(Vector2 movementInput, bool isRunning, bool aimInput)
    {
        // set move direction
        _lookDirection = _mainCamera.CameraLookDirection(_body, !isRunning && aimInput);
        _orientation.forward = _lookDirection.normalized;
        _moveDirection = _orientation.right * movementInput.x + _orientation.forward * movementInput.y;
        // move
        float speed = isRunning ? _runSpeed : _walkSpeed;
        _rigidBody.AddForce(_moveDirection * speed, ForceMode.Acceleration);
    }
    private void SetBodyMotionType(bool aimInput, bool shootInput, bool isArmed)
    {
        bool canShoot = (aimInput || shootInput) && isArmed;
        _body.forward = canShoot
            ? Vector3.Slerp(_body.forward, _lookDirection.normalized, _lerpBodyRotation * Time.deltaTime)
            : Vector3.Slerp(_body.forward, _moveDirection.normalized, _lerpBodyRotation * Time.deltaTime);
    }

    #region Body Rotation
    //private void CheckBodyAngle(Vector2 movementInput, bool isArmed)
    //{
    //    if (movementInput != Vector2.zero || !isArmed)
    //        return;

    //    var angle = Vector3.Angle(_body.forward, _orientation.forward);
    //    var crossProduct = Vector3.Cross(_body.forward, _orientation.forward);

    //    bool turnRight = crossProduct.y > 0 && angle > 45;
    //    bool turnLeft = crossProduct.y < 0 && angle > 45;

    //    if (!_isRotating)
    //    {
    //        if (turnRight)
    //        {
    //            OnTurnRight?.Invoke();
    //            BodyRotation(90f);
    //        }
    //        if (turnLeft)
    //        {
    //            OnTurnLeft?.Invoke();
    //            BodyRotation(-90f);
    //        }
    //    }
    //}
    //private async void BodyRotation(float value) 
    //{
    //    _isRotating = true;
    //    await Task.Delay(500); // wait for animation complete
    //    _body.Rotate(Vector3.up, value);
    //    _isRotating = false;
    //}
    #endregion
}