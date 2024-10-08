public abstract class BaseState
{
    protected IStateSwitcher _stateSwitcher;
    protected readonly AISharedContext _aiSharedContext;

    public BaseState(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext)
    {
        _stateSwitcher = stateSwitcher;
        _aiSharedContext = aiSharedContext;
    }

    public abstract AIState State { get; }
    public abstract void UpdateState();
    public virtual void Enter() { }
    public virtual void Exit() { }
}
public interface IFiniteStateMachine : IStateSwitcher
{
    void AddState(IState state);
    void UpdateState();
}
public interface IStateSwitcher
{
    void SwitchTo(AIState state);
}
public interface IState
{
    AIState State { get; }
    void Enter();
    void UpdateState();
    void Exit();
}
