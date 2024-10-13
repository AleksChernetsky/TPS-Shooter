using UnityEngine;
using UnityEngine.AI;

public class StateSearch : BaseState, IState
{
    public StateSearch(IStateSwitcher stateSwitcher, AISharedContext aiSharedContext) : base(stateSwitcher, aiSharedContext) { }
    public override AIState State => AIState.Search;

    private float _wanderRadius = 50f;
    private float _waitTime = 0.5f;
    private float _timer;

    public override void UpdateState()
    {
        if (_aiSharedContext.VitalitySystem.CurrentHealth <= 0)
        {
            _stateSwitcher.SwitchTo(AIState.Death);
            return;
        }

        if (_aiSharedContext.EnemyTracker.DistanceToEnemy <= _aiSharedContext.EnemyTracker.DistanceToCheck)
            _stateSwitcher.SwitchTo(AIState.Chase);

        if (!_aiSharedContext.NavMeshAgent.pathPending && _aiSharedContext.NavMeshAgent.remainingDistance <= _aiSharedContext.NavMeshAgent.stoppingDistance)
        {
            _timer += Time.deltaTime;
            if (_timer >= _waitTime)
            {
                MoveToRandomPoint();
                _timer = 0;
            }
        }
    }
    private void MoveToRandomPoint()
    {
        Vector3 randomDirection = new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z) * _wanderRadius;
        randomDirection += _aiSharedContext.NavMeshAgent.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, _wanderRadius, 1))
        {
            _aiSharedContext.NavMeshAgent.SetDestination(hit.position);
        }
    }
}