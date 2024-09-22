using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectileRigidbody;
    private float _timer;
    private float _timeToDestroy = 0.5f;

    public Rigidbody RigidBody => _projectileRigidbody;
    public int Damage { get; set; }
    [field: SerializeField] public int Speed { get; set; }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToDestroy)
        {
            ResetProjectile();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(Damage);
            ResetProjectile();
        }
        else
        {
            ResetProjectile();
        }
    }
    public void ResetProjectile()
    {
        _timer = 0;
        _projectileRigidbody.linearVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}