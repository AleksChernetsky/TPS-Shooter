using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _ragdolParts;

    private Animator _animator;
    private VitalitySystem _vitalitySystem;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _vitalitySystem = GetComponentInParent<VitalitySystem>();

        InitializeRagdoll();

        _vitalitySystem.OnDie += ActivateRagdoll;
    }
    private void InitializeRagdoll()
    {
        foreach (var part in _ragdolParts)
        {
            part.isKinematic = true;
            part.gameObject.AddComponent<RagdollPart>().Initialize(_vitalitySystem);
        }
    }
    private void ActivateRagdoll()
    {
        _animator.enabled = false;
        foreach (var part in _ragdolParts)
        {
            part.isKinematic = false;
        }
    }
}
public class RagdollPart : MonoBehaviour
{
    private VitalitySystem _vitalitySystem;
    public void Initialize(VitalitySystem vitalitySystem) => _vitalitySystem = vitalitySystem;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
            _vitalitySystem.TakeDamage(projectile.Damage);
    }
}