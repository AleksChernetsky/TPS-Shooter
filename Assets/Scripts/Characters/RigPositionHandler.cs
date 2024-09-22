using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigPositionHandler : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint _leftHandBone;
    private RigBuilder _rigBuilder;
    private float _transitionSpeed = 10f;

    [Header("Rigs")]
    private Rig _aimRig; private readonly string AimRig = "AimRig";
    private Rig _leftHandRig; private readonly string LeftHandRig = "LeftHandRig";

    private void Start()
    {
        _rigBuilder = GetComponent<RigBuilder>();
        _rigBuilder.Build();

        foreach (var bone in _rigBuilder.layers)
        {
            if (bone.name == AimRig)
                _aimRig = bone.rig;
            if (bone.name == LeftHandRig)
                _leftHandRig = bone.rig;
        }
    }
    public void SetLeftHand(Weapon weapon)
    {
        _leftHandBone.data.target = weapon != null ? weapon.LeftHandPlace : null;
        _leftHandRig.weight = weapon != null ? 1 : 0;
        _rigBuilder.Build();
    }
    public void SetAimRig(bool isAiming)
    {
        _aimRig.weight = isAiming
            ? Mathf.Lerp(_aimRig.weight, 1f, _transitionSpeed * Time.deltaTime)
            : Mathf.Lerp(_aimRig.weight, 0f, _transitionSpeed * Time.deltaTime);
    }
}