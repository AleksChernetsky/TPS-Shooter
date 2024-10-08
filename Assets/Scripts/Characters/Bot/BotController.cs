using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    [Header("Input")]
    Vector2 MovementInput => _agent.desiredVelocity;
    bool RunInput => _agent.desiredVelocity.magnitude > 0 ? true : false;

    [Header("Combat")]
    [SerializeField] private Transform _weaponHolder;
    private WeaponHandler _weaponHandler;

    private NavMeshAgent _agent;
    private RigController _rigController;
    private Animator _animator;
    private AnimationHandler _animHandler;
    private EnemyTracker _enemyTracker;

    [Header("StateMachine")]
    private State _state;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyTracker = GetComponent<EnemyTracker>();
        _rigController = GetComponentInChildren<RigController>();
        _animator = GetComponentInChildren<Animator>();

        _weaponHandler = new WeaponHandler(_weaponHolder, _rigController);
        _animHandler = new AnimationHandler(_animator, _weaponHandler);
        _state = new State(_weaponHandler, _enemyTracker, _agent);
    }
    private void OnEnable()
    {
        _animHandler.Initialize();
        _rigController.Initialize();
        _weaponHandler.Initialize();
        _state.Initialize();
    }
    private void Update()
    {
        // animations
        _animHandler.UpdateAnimations(MovementInput, RunInput);
        // rigs
        _rigController.SetAimRig(_weaponHandler.IsAiming || _weaponHandler.IsShooting);
        // state machine
        _state.Update();
    }
}