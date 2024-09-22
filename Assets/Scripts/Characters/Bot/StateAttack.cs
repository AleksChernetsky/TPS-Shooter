public class StateAttack : BaseState
{
    public StateAttack(StateMachine stateMachine, BotActions botActions, VitalitySystem vitalitySystem, EnemyTracker enemyTracker)
        : base(stateMachine, botActions, vitalitySystem, enemyTracker) { }

    public override void UpdateState()
    {
        if (_botActions.CanAttack || _botActions.EnemyAlive)
        {
            _botActions.Attack();
        }
        else
        {
            _stateMachine.SwitchState(_botActions.StateMove);
        }
    }
}