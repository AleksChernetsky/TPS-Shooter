using System;

using UnityEngine.AI;
public enum AIState
{
    Search,
    Chase,
    Attack
}
public class State
{
    private WeaponHandler _weaponHandler;
    private EnemyTracker _enemyTracker;
    private NavMeshAgent _navMeshAgent;

    private AIState _aiDefaultState;
    private IFiniteStateMachine _finiteStateMachine;

    private AISharedContext _aiSharedContext;

    public State(WeaponHandler weaponHandler, EnemyTracker enemyTracker, NavMeshAgent navMeshAgent)
    {
        _weaponHandler = weaponHandler;
        _enemyTracker = enemyTracker;
        _navMeshAgent = navMeshAgent;
    }

    public void Initialize()
    {
        PrepareSharedContext();
        PrepareStateMachine();
        SwitchToDefaultState();
    }
    public void Update() => _finiteStateMachine.UpdateState();

    private void PrepareSharedContext() => _aiSharedContext = new AISharedContext(_weaponHandler, _enemyTracker, _navMeshAgent);
    private void PrepareStateMachine()
    {
        _finiteStateMachine = new FiniteStateMachine(Array.Empty<IState>());
        _finiteStateMachine.AddState(new StateSearch(_finiteStateMachine, _aiSharedContext));
        _finiteStateMachine.AddState(new StateChase(_finiteStateMachine, _aiSharedContext));
        _finiteStateMachine.AddState(new StateAttack(_finiteStateMachine, _aiSharedContext));
    }
    private void SwitchToDefaultState() => _finiteStateMachine.SwitchTo(_aiDefaultState);
}
