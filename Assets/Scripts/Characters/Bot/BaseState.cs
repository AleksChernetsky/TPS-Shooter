public class BaseState
{
    protected StateMachine _stateMachine;
    protected BotActions _botActions;
    protected VitalitySystem _vitalitySystem;
    protected EnemyTracker _enemyTracker;

    public BaseState(StateMachine stateMachine, BotActions botActions, VitalitySystem vitalitySystem, EnemyTracker enemyTracker)
    {
        _stateMachine = stateMachine;
        _botActions = botActions;
        _vitalitySystem = vitalitySystem;
        _enemyTracker = enemyTracker;
    }

    public virtual void UpdateState() { }
}
