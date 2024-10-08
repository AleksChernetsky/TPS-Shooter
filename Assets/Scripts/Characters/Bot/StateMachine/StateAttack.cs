public class StateAttack : BaseState, IState
{
    public StateAttack(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext) : base(stateSwitcher, aiSharedContext) { }
    public override AIState State => AIState.Attack;
    public override void UpdateState()
    {
        if (_aiSharedContext.EnemyTracker.Enemy != null)
        {
            _aiSharedContext.NavMeshAgent.isStopped = true;
            _aiSharedContext.EnemyTracker.FaceToEnemy();

            if (!_aiSharedContext.WeaponHandler.IsShooting)
                _aiSharedContext.WeaponHandler.Shoot(true);
        }

        if (_aiSharedContext.EnemyTracker.DistanceToEnemy >= _aiSharedContext.EnemyTracker.DistanceToAttack)
        {
            _aiSharedContext.NavMeshAgent.isStopped = false;
            _aiSharedContext.WeaponHandler.Shoot(false);
            _stateSwitcher.SwitchTo(AIState.Chase);
        }
    }
}