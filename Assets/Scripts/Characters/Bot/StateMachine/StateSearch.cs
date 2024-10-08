using UnityEngine;

public class StateSearch : BaseState, IState
{
    public StateSearch(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext) : base(stateSwitcher, aiSharedContext) { }

    public override AIState State => AIState.Search;
    public override void UpdateState()
    {
        Debug.Log("State Search");

        if (_aiSharedContext.EnemyTracker.DistanceToEnemy <= _aiSharedContext.EnemyTracker.DistanceToCheck)
            _stateSwitcher.SwitchTo(AIState.Chase);
    }
}
