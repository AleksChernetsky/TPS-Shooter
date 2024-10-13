using UnityEngine;

public class StateSearch : BaseState, IState
{
    public StateSearch(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext) : base(stateSwitcher, aiSharedContext) { }

    public override AIState State => AIState.Search;
    public override void UpdateState()
    {
        if (_aiSharedContext.VitalitySystem.CurrentHealth <= 0)
        {
            _stateSwitcher.SwitchTo(AIState.Death);
            return;
        }

        if (_aiSharedContext.EnemyTracker.DistanceToEnemy <= _aiSharedContext.EnemyTracker.DistanceToCheck)
            _stateSwitcher.SwitchTo(AIState.Chase);
    }
}