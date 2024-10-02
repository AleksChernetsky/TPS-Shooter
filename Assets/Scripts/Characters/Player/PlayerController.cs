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
    private IKController _ikController;
    private Animator _animator;
    private AnimationHandler _animHandler;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _ikController = GetComponentInChildren<IKController>();
        _animator = GetComponentInChildren<Animator>();

        _inputService = new InputService();
        _movement = new Movement(_body, _orientation, _mainCamera, _rigidBody);
        _weaponHandler = new WeaponHandler(_weaponHolder, _ikController, _inputService);
        _animHandler = new AnimationHandler(_animator, _inputService, _weaponHandler);
    }
    private void OnEnable()
    {
        _inputService.Initialize();
        _animHandler.Initialize();
        _weaponHandler.Initialize();
    }
    private void Start()
    {
        _inputService.OnInteractInput += Interact;
        _inputService.OnChooseWeapon += ChooseWeapon;
    }
    private void OnDisable()
    {
        _inputService.OnDisable();
    }
    private void Update()
    {
        // animations
        _animHandler.UpdateAnimations();
    }
    private void FixedUpdate()
    {
        // movement
        _movement.Move(MovementInput, RunInput);
    }
    private void ChooseWeapon(int weaponIndex)
    {
        _weaponHandler.TakeWeapon(weaponIndex);
    }
    private void Interact()
    {
        if (_mainCamera.RaycastHit.collider.TryGetComponent(out IWeaponInteractable interactable))
        {
            if (_mainCamera.RaycastHit.distance <= 5f)
                _weaponHandler.EquipNewWeapon(interactable.WeaponInteract());
        }
    }
}
