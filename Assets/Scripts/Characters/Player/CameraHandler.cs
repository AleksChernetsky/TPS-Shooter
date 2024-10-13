using Unity.Cinemachine;

using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _normalCamera;
    [SerializeField] private CinemachinePanTilt _combatCamera;
    [SerializeField] private LayerMask _ignorLayer;
    private RaycastHit _raycastHit;
    private Vector2 _screenCenter;

    [Header("Sense")]
    [SerializeField] private float _normalSence;
    [SerializeField] private float _aimedSence;
    private CinemachineInputAxisController _normalAxisController;
    private CinemachineInputAxisController _combatAxisController;

    [field: SerializeField] public Transform LookAtPoint { get; private set; }
    public RaycastHit RaycastHit => _raycastHit;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _normalAxisController = _normalCamera.GetComponent<CinemachineInputAxisController>();
        _combatAxisController = _combatCamera.GetComponent<CinemachineInputAxisController>();

        CameraSence();
    }

    private void Update()
    {
        SetAimPointPosition();
    }

    private void SetAimPointPosition()
    {
        _screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);

        if (Physics.Raycast(ray, out _raycastHit, Mathf.Infinity, ~_ignorLayer))
            LookAtPoint.position = Vector3.Lerp(LookAtPoint.position, RaycastHit.point, 0.5f);
    }
    public void SetCombatCamera(Transform camera, int magnification)
    {
        var currentCamera = _combatCamera.GetComponent<CinemachineCamera>();
        currentCamera.Target.TrackingTarget = camera;
        currentCamera.Lens.FieldOfView = magnification;
    }
    public void SetCameraMode(bool aimed)
    {
        _normalCamera.gameObject.SetActive(!aimed);
        _combatCamera.gameObject.SetActive(aimed);
    }
    public Vector3 CameraLookDirection(Transform body, bool condition)
    {
        if (condition)
        {
            _normalCamera.TiltAxis.Value = _combatCamera.TiltAxis.Value;
            _normalCamera.PanAxis.Value = _combatCamera.PanAxis.Value;
        }
        else
        {
            _combatCamera.TiltAxis.Value = _normalCamera.TiltAxis.Value;
            _combatCamera.PanAxis.Value = _normalCamera.PanAxis.Value;
        }
        return condition == false
            ? body.position - new Vector3(_normalCamera.transform.position.x, body.position.y, _normalCamera.transform.position.z)
            : new Vector3(_combatCamera.transform.forward.x, body.position.y, _combatCamera.transform.forward.z);
    }
    public void CameraSence()
    {
        for (int i = 0; i < _normalAxisController.Controllers.Count; i++)
            _normalAxisController.Controllers[i].Input.Gain = i == 0 ? _normalSence : -_normalSence; // 0 = X Axis, 1 = Y Axis

        for (int i = 0; i < _combatAxisController.Controllers.Count; i++)
            _combatAxisController.Controllers[i].Input.Gain = i == 0 ? _aimedSence : -_aimedSence; // 0 = X Axis, 1 = Y Axis
    }
}
