using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AI;

public class BotActions : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    private List<Transform> _remainPoints = new List<Transform>();

    private VitalitySystem _vitalitySystem;
    private EnemyTracker _enemyTracker;
    private NavMeshAgent _agent;
    private Weapon _weapon;

    public bool EnemyAlive => _enemyTracker.Enemy != null;
    public bool CanAttack => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToAttack && !_enemyTracker.EnemyBlocked;
    public bool TargetLost => _enemyTracker.DistanceToEnemy > _enemyTracker.DistanceToCheck || !EnemyAlive;
    public bool CanChase => _enemyTracker.DistanceToEnemy <= _enemyTracker.DistanceToCheck;

    public StateMachine StateMachine { get; set; }
    public StateMove StateMove { get; set; }
    public StateAttack StateAttack { get; set; }

    private void Awake()
    {
        _vitalitySystem = GetComponent<VitalitySystem>();
        _enemyTracker = GetComponent<EnemyTracker>();
        _agent = GetComponent<NavMeshAgent>();
        _weapon = GetComponent<Weapon>();

        _remainPoints.AddRange(_patrolPoints);

        StateMachine = new StateMachine();
        StateMove = new StateMove(StateMachine, this, _vitalitySystem, _enemyTracker);
        StateAttack = new StateAttack(StateMachine, this, _vitalitySystem, _enemyTracker);
    }
    private void Start()
    {
        StateMachine.Initialize(StateMove);

        _vitalitySystem.OnDie += DeathState;
    }
    private void Update()
    {
        if (StateMachine.CurrentState != null)
        {
            StateMachine.CurrentState.UpdateState();
        }
    }

    public void Move()
    {
        _agent.isStopped = false;
        if (!_agent.hasPath)
        {
            int randomPoint = Random.Range(0, _remainPoints.Count);
            _agent.SetDestination(_remainPoints[randomPoint].position);

            _remainPoints.RemoveAt(randomPoint);
            if (_remainPoints.Count == 0)
            {
                _remainPoints.AddRange(_patrolPoints);
            }
        }
    }
    public void Move(Vector3 direction)
    {
        _agent.isStopped = false;
        _agent.SetDestination(direction);
    }

    public void Attack()
    {
        if (EnemyAlive)
        {
            transform.LookAt(_enemyTracker.Enemy.position);
            _agent.isStopped = true;
            //_weapon.PerformAttack(true);
        }
    }
    private void DeathState()
    {
        StateMachine.CurrentState = null;
    }
}
