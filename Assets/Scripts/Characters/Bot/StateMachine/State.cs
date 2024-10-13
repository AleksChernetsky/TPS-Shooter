using System;

using UnityEngine.AI;
public enum AIState
{
    Search,
    Chase,
    Attack,
    Death
}
public class State
{
    private WeaponHandler _weaponHandler;
    private EnemyTracker _enemyTracker;
    private NavMeshAgent _navMeshAgent;
    private VitalitySystem _vitalitySystem;

    private AIState _aiDefaultState;
    private IFiniteStateMachine _finiteStateMachine;

    private AISharedContext _aiSharedContext;

    public State(WeaponHandler weaponHandler, EnemyTracker enemyTracker, NavMeshAgent navMeshAgent, VitalitySystem vitalitySystem)
    {
        _weaponHandler = weaponHandler;
        _enemyTracker = enemyTracker;
        _navMeshAgent = navMeshAgent;
        _vitalitySystem = vitalitySystem;
    }

    public void Initialize()
    {
        PrepareSharedContext();
        PrepareStateMachine();
        SwitchToDefaultState();
    }
    public void Update() => _finiteStateMachine.UpdateState();

    private void PrepareSharedContext() => _aiSharedContext = new AISharedContext(_weaponHandler, _enemyTracker, _navMeshAgent, _vitalitySystem);
    private void PrepareStateMachine()
    {
        _finiteStateMachine = new FiniteStateMachine(Array.Empty<IState>());
        _finiteStateMachine.AddState(new StateSearch(_finiteStateMachine, _aiSharedContext));
        _finiteStateMachine.AddState(new StateChase(_finiteStateMachine, _aiSharedContext));
        _finiteStateMachine.AddState(new StateAttack(_finiteStateMachine, _aiSharedContext));
        _finiteStateMachine.AddState(new StateDeath(_finiteStateMachine, _aiSharedContext));
    }
    private void SwitchToDefaultState() => _finiteStateMachine.SwitchTo(_aiDefaultState);
}
