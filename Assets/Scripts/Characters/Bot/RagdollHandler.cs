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

        SetRagdollActive(false);

        _vitalitySystem.OnDie += ActivateRagdoll;
    }

    public void ActivateRagdoll()
    {
        _animator.enabled = false;
        SetRagdollActive(true);
    }

    void SetRagdollActive(bool active)
    {
        foreach (var rb in _ragdolParts)
            rb.isKinematic = !active;
    }
}