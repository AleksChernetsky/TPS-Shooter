using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _orientation;
    [SerializeField] private CameraHandler _mainCamera;

    [Header("Input")]
    private InputService _inputService;
    Vector2 MovementInput => _inputService.MovementInput;
    bool RunInput => _inputService.RunInput;

    [Header("Movement")]
    private Movement _movement;

    [Header("Combat")]
    [SerializeField] private Transform _weaponHolder;
    private WeaponHandler _weaponHandler;

    private Rigidbody _rigidBody;
    private RigController _rigController;
    private Animator _animator;
    private AnimationHandler _animHandler;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigController = GetComponentInChildren<RigController>();
        _animator = GetComponentInChildren<Animator>();

        _inputService = new InputService();
        _weaponHandler = new WeaponHandler(_weaponHolder, _rigController);
        _movement = new Movement(_body, _orientation, _mainCamera, _rigidBody);
        _animHandler = new AnimationHandler(_animator, _weaponHandler);
    }
    private void OnEnable()
    {
        _inputService.Initialize();
        _animHandler.Initialize();
        _rigController.Initialize();
        _weaponHandler.Initialize();

        _inputService.OnShootInput += Shoot;
        _inputService.OnAimInput += Aim;
        _inputService.OnHideWeaponInput += HideWeapon;
        _inputService.OnInteractInput += Interact;
        _inputService.OnChooseWeapon += ChooseWeapon;
    }
    private void Update()
    {
        // animations
        _animHandler.UpdateAnimations(MovementInput, RunInput);
        // rigs
        _rigController.SetAimRig(_weaponHandler.IsAiming || _weaponHandler.IsShooting);
    }
    private void FixedUpdate()
    {
        // movement
        _movement.UpdateMovement(MovementInput, RunInput, _weaponHandler);
    }
    private void LateUpdate()
    {
        // camera
        _mainCamera.SetCombatCamera(!RunInput && _weaponHandler.IsArmed && _weaponHandler.IsAiming);
    }
    private void OnDisable()
    {
        _inputService.OnShootInput -= Shoot;
        _inputService.OnAimInput -= Aim;
        _inputService.OnHideWeaponInput -= HideWeapon;
        _inputService.OnInteractInput -= Interact;
        _inputService.OnChooseWeapon -= ChooseWeapon;
        _inputService.OnDisable();
    }
    private void Shoot(bool shootInput) => _weaponHandler.Shoot(shootInput);
    private void Aim(bool aimInput) => _weaponHandler.Aim(RunInput, aimInput);
    private void HideWeapon() => _weaponHandler.HideWeapon();
    private void ChooseWeapon(int weaponIndex) => _weaponHandler.TakeWeapon(weaponIndex);
    private void Interact()
    {
        if (_mainCamera.RaycastHit.collider.TryGetComponent(out IWeaponInteractable interactable))
        {
            if (_mainCamera.RaycastHit.distance <= 4f)
                _weaponHandler.EquipNewWeapon(interactable.WeaponInteract());
        }
    }
}
