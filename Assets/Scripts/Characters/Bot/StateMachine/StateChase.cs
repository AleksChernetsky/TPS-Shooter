using UnityEngine;

public class StateChase : BaseState, IState
{
    public StateChase(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext) : base(stateSwitcher, aiSharedContext) { }

    public override AIState State => AIState.Chase;
    public override void UpdateState()
    {
        if (_aiSharedContext.EnemyTracker.Enemy != null)
        _aiSharedContext.NavMeshAgent.SetDestination(_aiSharedContext.EnemyTracker.Enemy.position);

        if (_aiSharedContext.EnemyTracker.DistanceToEnemy <= _aiSharedContext.EnemyTracker.DistanceToAttack)
            _stateSwitcher.SwitchTo(AIState.Attack);
        else if (_aiSharedContext.EnemyTracker.DistanceToEnemy >= _aiSharedContext.EnemyTracker.DistanceToCheck)
            _stateSwitcher.SwitchTo(AIState.Search);
    }
}
