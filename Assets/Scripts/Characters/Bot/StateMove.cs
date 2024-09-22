public class StateMove : BaseState
{
    public StateMove(StateMachine stateMachine, BotActions botActions, VitalitySystem vitalitySystem, EnemyTracker enemyTracker)
        : base(stateMachine, botActions, vitalitySystem, enemyTracker) { }

    public override void UpdateState()
    {
        _botActions.Move();
        if (_botActions.CanChase && _botActions.EnemyAlive)
        {
            _botActions.Move(_enemyTracker.Enemy.position);
        }
        if (_botActions.CanAttack)
        {
            _stateMachine.SwitchState(_botActions.StateAttack);
        }
    }
}