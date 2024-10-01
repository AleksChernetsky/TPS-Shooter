using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private Transform _lookAtTarget;
    private Animator _animator;
    private float _ikWeight;

    public Transform RightHandTarget { get; private set; }
    public Transform LeftHandTarget { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIKWeight(float weight, Transform rightHand, Transform leftHand)
    {
        _ikWeight = weight;
        RightHandTarget = rightHand;
        LeftHandTarget = leftHand;
    }

    private void OnAnimatorIK()
    {
        if (RightHandTarget != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _ikWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _ikWeight);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);
        }
        if (LeftHandTarget != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _ikWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _ikWeight);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.rotation);
        }
        if (_lookAtTarget != null)
        {
            _animator.SetLookAtWeight(_ikWeight);
            _animator.SetLookAtPosition(_lookAtTarget.position);
        }
    }
}