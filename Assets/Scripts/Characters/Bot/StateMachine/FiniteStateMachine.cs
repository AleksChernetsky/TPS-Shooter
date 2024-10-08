using System.Collections.Generic;

using UnityEngine;

public class FiniteStateMachine : IFiniteStateMachine
{
    private Dictionary<AIState, IState> _aiStates = new Dictionary<AIState, IState>();
    private IState _current;

    public FiniteStateMachine(params IState[] states)
    {
        foreach (var state in states)
        {
            _aiStates[state.State] = state;
        }
    }
    public void AddState(IState state)
    {
        if (_aiStates.ContainsKey(state.State))
        {
            Debug.LogError($"[{GetType().Name}][AddState] Trying to add state that already exist {state.State}");
            return;
        }

        _aiStates[state.State] = state;
    }
    public void SwitchTo(AIState state)
    {
        if (_aiStates.ContainsKey(state) == false)
        {
            Debug.LogError($"[{GetType().Name}][AddState] Trying to switch to state that did not exist {state}");
            return;
        }

        if (_current != null)
            _current.Exit();

        _current = _aiStates[state];
        _current.Enter();
    }
    public void UpdateState()
    {
        _current.UpdateState();
    }
}
