using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _orientation;
    private CharacterController _controller;
    private float _walkSpeed = 2f;
    private float _runSpeed = 4f;
    private float _lerpBodyRotation = 9f;
    private Vector3 _moveDirection;

    [Header("Aim")]
    [SerializeField] private TPSCamera _tpsCamera;
    [SerializeField] private RigPositionHandler _rigHandler;
    private Vector3 _lookDirection;

    [Header("Weapon")]
    private Weapon _weapon;

    public bool IsArmed => _weapon != null;

    private AnimationService _animationService;
    private PlayerInputService _playerinput;
    private EquipWeaponHandler _equipWeapon;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _controller = GetComponent<CharacterController>();
        _animationService = GetComponentInChildren<AnimationService>();
        _playerinput = GetComponent<PlayerInputService>();
        _equipWeapon = GetComponentInChildren<EquipWeaponHandler>();
    }
    private void Start()
    {
        _playerinput.OnInteractInput += EquipNewWeapon;
        _playerinput.OnHideWeaponInput += HideWeapon;
        _playerinput.OnChooseWeapon += ChooseWeapon;
    }
    private void Update()
    {
        Movement(_playerinput.MovementInput, _playerinput.RunInput, _playerinput.CrouchInput);
        SetBodyMotionType(_playerinput.AimInput, _playerinput.ShootInput);
        Aim(_playerinput.AimInput, _playerinput.RunInput);
        Shoot(_playerinput.ShootInput, _playerinput.AimInput);
    }

    private void Movement(Vector3 inputDirection, bool runInput, bool crouchInput)
    {
        // set move direction
        _lookDirection = new Vector3(_tpsCamera.AimPoint.position.x, _body.position.y, _tpsCamera.AimPoint.position.z) - transform.position;
        _orientation.forward = _lookDirection.normalized;
        _moveDirection = _orientation.right * inputDirection.x + _orientation.forward * inputDirection.y;
        // move
        float moveSpeed = runInput ? _runSpeed : _walkSpeed;
        _controller.Move(_moveDirection * moveSpeed * Time.deltaTime);
        // anim
        _animationService.MovementAnim(inputDirection, runInput, crouchInput, IsArmed);
    }
    private void SetBodyMotionType(bool aimInput, bool shootInput)
    {
        bool canShoot = (aimInput || shootInput) && IsArmed;
        _body.forward = canShoot
            ? Vector3.Slerp(_body.forward, _lookDirection.normalized, _lerpBodyRotation * Time.deltaTime)
            : Vector3.Slerp(_body.forward, _moveDirection.normalized, _lerpBodyRotation * Time.deltaTime);

        _rigHandler.SetAimRig(IsArmed);
        _animationService.MotionTypeLayer(canShoot);
    }
    private void Aim(bool aimInput, bool runInput)
    {
        // set camera
        _tpsCamera.CameraSence(aimInput && IsArmed);
        _tpsCamera.CombatCamera.SetActive(aimInput && !runInput && IsArmed);
        // set animations
        _animationService.PlayAimAnim(aimInput && !runInput && IsArmed);
    }
    private void Shoot(bool shootInput, bool aimInput)
    {
        if (!IsArmed) return;

        if (shootInput)
            _weapon.PerformAttack(aimInput);

        _animationService.PlayShootAnim(shootInput);
    }
    private void EquipNewWeapon()
    {
        _equipWeapon.EquipNewWeapon(_tpsCamera.RaycastHit);
    }
    private void ChooseWeapon(int weaponIndex)
    {
        if (IsArmed)
            _equipWeapon.HideWeapon(_weapon);

        _weapon = _equipWeapon.TakeWeapon(weaponIndex);
    }
    private void HideWeapon()
    {
        _equipWeapon.HideWeapon(_weapon);
        _weapon = null;
    }
}
