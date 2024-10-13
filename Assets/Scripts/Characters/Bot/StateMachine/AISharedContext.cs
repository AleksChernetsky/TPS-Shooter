using UnityEngine.AI;

public class AISharedContext
{
    public WeaponHandler WeaponHandler { get; private set; }
    public EnemyTracker EnemyTracker { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public VitalitySystem VitalitySystem { get; private set; }

    public AISharedContext(WeaponHandler weaponHandler, EnemyTracker enemyTracker, NavMeshAgent navMeshAgent, VitalitySystem vitalitySystem)
    {
        WeaponHandler = weaponHandler;
        EnemyTracker = enemyTracker;
        NavMeshAgent = navMeshAgent;
        VitalitySystem = vitalitySystem;
    }
}
