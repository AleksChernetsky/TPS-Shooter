using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    [Header("Rigs")]
    private Rig _aimRig; private readonly string AimRig = "AimRig";
    private Rig _leftHandRig; private readonly string LeftHandRig = "LeftHandRig";

    [Header("Rig Values")]
    [SerializeField] private TwoBoneIKConstraint _leftHandBone;
    private RigBuilder _rigBuilder;

    public void Initialize()
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
    public void SetHandsWeight(Weapon weapon)
    {
        _leftHandBone.data.target = weapon != null ? weapon.LeftHandTarget : null;
        _leftHandRig.weight = weapon != null ? 1 : 0;

        _rigBuilder.Build();
    }
    public void SetAimRig(bool condition)
    {
        _aimRig.weight = condition ? 1 : 0;
    }
}
