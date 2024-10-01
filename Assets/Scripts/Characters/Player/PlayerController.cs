using UnityEngine;

[RequireComponent(typeof(InputService), typeof(Rigidbody))]
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
    bool IsGrounded => _movement.IsGrounded;

    [Header("Combat")]
    [SerializeField] private Transform _weaponHolder;
    private WeaponHandler _weaponHandler;

    private Rigidbody _rigidBody;
    private IKController _ikController;
    private Animator _animator;
    private AnimationHandler _animHandler;

    private void Start()
    {
        _inputService = GetComponent<InputService>();
        _rigidBody = GetComponent<Rigidbody>();
        _ikController = GetComponentInChildren<IKController>();
        _animator = GetComponentInChildren<Animator>();

        _movement = new Movement(_body, _orientation, _mainCamera, _rigidBody);
        _animHandler = new AnimationHandler(_animator);
        _weaponHandler = new WeaponHandler(_weaponHolder, _ikController);

        _animHandler.Initialize();

        _inputService.OnInteractInput += Interract;
        _inputService.OnChooseWeapon += ChooseWeapon;
    }

    private void Update()
    {
        // animations
        _animHandler.MovementAnim(MovementInput);
        _animHandler.SetMotionType(MovementInput, _weaponHandler.IsArmed, RunInput, IsGrounded);
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
    private void Interract()
    {
        if (_mainCamera.RaycastHit.collider.TryGetComponent(out IWeaponInterractable interractable))
        {
            if (_mainCamera.RaycastHit.distance <= 5f)
                _weaponHandler.EquipNewWeapon(interractable.WeaponInterract());           
        }
    }
}
