public class StateDeath : BaseState, IState
{
    public StateDeath(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext) : base(stateSwitcher, aiSharedContext) { }

    public override AIState State => AIState.Death;
    public override void UpdateState()
    {
        _aiSharedContext.NavMeshAgent.isStopped = true;        
    }
}
