using Unity.Cinemachine;

using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _normalCamera;
    [SerializeField] private CinemachinePanTilt _combatCamera;
    [SerializeField] private Vector2 _normalSence;
    [SerializeField] private Vector2 _aimedSence;
    [SerializeField] private LayerMask _ignorLayer;
    private RaycastHit _raycastHit;
    private Vector2 _screenCenter;
    private CinemachineInputAxisController _axisController;
    private bool _isAiming;

    [field: SerializeField] public Transform AimPoint { get; private set; }
    public Transform NormalCamera { get; private set; }
    public GameObject CombatCamera { get; private set; }
    public RaycastHit RaycastHit => _raycastHit;

    private void Awake()
    {
        _axisController = _normalCamera.GetComponent<CinemachineInputAxisController>();
        NormalCamera = _normalCamera.transform;
        CombatCamera = _combatCamera.gameObject;
    }
    private void FixedUpdate() => SetAimPointPosition();

    private void SetAimPointPosition()
    {
        _screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);

        if (Physics.Raycast(ray, out _raycastHit, Mathf.Infinity, ~_ignorLayer))
            AimPoint.position = Vector3.Lerp(AimPoint.position, RaycastHit.point, 0.5f);
    }
    public void SetCamera(bool aimInput)
    {
        _isAiming = aimInput;

        _normalCamera.gameObject.SetActive(!_isAiming);
        _combatCamera.gameObject.SetActive(_isAiming);

        foreach (var axis in _axisController.Controllers)
        {
            if (axis.Name == "Look Orbit X")
            {
                axis.Input.Gain = _isAiming ? _aimedSence.x : _normalSence.x;
            }
            if (axis.Name == "Look Orbit Y")
            {
                axis.Input.Gain = _isAiming ? -_aimedSence.y : -_normalSence.y;
            }
        }
    }
    public Vector3 CameraLookDirection(Transform body)
    {
        if (_isAiming)
        {
            _normalCamera.TiltAxis.Value = _combatCamera.TiltAxis.Value;
            _normalCamera.PanAxis.Value = _combatCamera.PanAxis.Value;
        }
        else
        {
            _combatCamera.TiltAxis.Value = _normalCamera.TiltAxis.Value;
            _combatCamera.PanAxis.Value = _normalCamera.PanAxis.Value;
        }

        return _isAiming == false
            ? body.position - new Vector3(NormalCamera.position.x, body.position.y, NormalCamera.position.z)
            : new Vector3(CombatCamera.transform.forward.x, body.position.y, CombatCamera.transform.forward.z);
    }
}