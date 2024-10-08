using UnityEngine;
using UnityEngine.AI;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField] private Transform _lookAtPoint;
    private float _checkCollidersDelay;
    private float _distanceToTarget;
    private Collider[] _enemyColliders;

    [Header("Enemy Check Values")]
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float _distanceToCheck;
    [SerializeField] private float _distanceToAttack;

    public Transform Enemy { get; private set; }
    public bool EnemyBlocked { get; private set; }
    public float DistanceToEnemy { get; private set; }
    public float DistanceToCheck { get => _distanceToCheck; set => _distanceToCheck = value; }
    public float DistanceToAttack { get => _distanceToAttack; set => _distanceToAttack = value; }

    private void OnDrawGizmos()
    {
        if (Enemy == null) Gizmos.color = Color.white;
        else Gizmos.color = Color.red;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DistanceToCheck);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceToAttack);
    }

    private void Awake() => DistanceToEnemy = Mathf.Infinity;

    private void Update() => GetTarget();

    private void GetTarget()
    {
        _checkCollidersDelay += Time.deltaTime;
        if (_checkCollidersDelay >= 0.25f)
        {
            _enemyColliders = Physics.OverlapSphere(transform.position, DistanceToCheck, layermask);
            EnemySight();
            Enemy = NearestTarget();
            _checkCollidersDelay = 0;
        }
    }
    private Transform NearestTarget()
    {
        DistanceToEnemy = Mathf.Infinity;
        Transform target = null;

        for (var i = 0; i < _enemyColliders.Length; i++)
        {
            if (_enemyColliders[i].transform == transform)
                continue;

            _distanceToTarget = Vector3.Distance(transform.position, _enemyColliders[i].transform.position);

            if (_distanceToTarget < DistanceToEnemy)
            {
                DistanceToEnemy = _distanceToTarget;
                target = _enemyColliders[i].transform;
            }
        }
        return target;
    }
    private void EnemySight()
    {
        NavMeshHit hit;
        if (Enemy != null)
        {
            EnemyBlocked = NavMesh.Raycast(transform.position, Enemy.transform.position, out hit, NavMesh.AllAreas);
            Debug.DrawLine(transform.position, Enemy.transform.position, EnemyBlocked ? Color.red : Color.green);

            if (EnemyBlocked)
                Debug.DrawRay(hit.position, Vector3.up, Color.yellow, 0.5f);
        }
    }
    public void FaceToEnemy()
    {
        transform.LookAt(Enemy);
        _lookAtPoint.position = Vector3.Slerp
            (_lookAtPoint.position,
            new Vector3(Enemy.position.x + Random.Range(-1f, 1f), Enemy.position.y + Random.Range(0f, 2f), Enemy.position.z),
            5f * Time.deltaTime);
    }
}