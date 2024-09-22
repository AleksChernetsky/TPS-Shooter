using Unity.Cinemachine;

using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField] private CinemachineOrbitalFollow _normalCamera;
    [SerializeField] private CinemachineOrbitalFollow _combatCamera;
    [SerializeField] private Vector2 _normalSence;
    [SerializeField] private Vector2 _aimedSence;
    [SerializeField] private LayerMask _ignorLayer;
    private RaycastHit _raycastHit;
    private CinemachineInputAxisController _axisController;

    [field: SerializeField] public Transform AimPoint { get; private set; }
    public GameObject CombatCamera => _combatCamera.gameObject;
    public RaycastHit RaycastHit => _raycastHit;

    private void Awake() => _axisController = _normalCamera.GetComponent<CinemachineInputAxisController>();
    private void Update() => SetAimPointPosition();
    private void LateUpdate() => StableCameraSwitch();

    private void SetAimPointPosition()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out _raycastHit, Mathf.Infinity, ~_ignorLayer))
            AimPoint.position = Vector3.Lerp(AimPoint.position, RaycastHit.point, 0.75f);
    }
    private void StableCameraSwitch()
    {
        _combatCamera.HorizontalAxis = _normalCamera.HorizontalAxis;
        _combatCamera.VerticalAxis = _normalCamera.VerticalAxis;
    }
    public void CameraSence(bool AimInput)
    {
        foreach (var axis in _axisController.Controllers)
        {
            if (axis.Name == "Look Orbit X")
            {
                axis.Input.Gain = AimInput ? _aimedSence.x : _normalSence.x;
            }
            if (axis.Name == "Look Orbit Y")
            {
                axis.Input.Gain = AimInput ? -_aimedSence.y : -_normalSence.y;
            }
        }
    }
}