using Unity.Cinemachine;

using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _normalCamera;
    [SerializeField] private CinemachinePanTilt _combatCamera;
    [SerializeField] private LayerMask _ignorLayer;
    private RaycastHit _raycastHit;
    private Vector2 _screenCenter;

    [field: SerializeField] public Transform LookAtPoint { get; private set; }
    public RaycastHit RaycastHit => _raycastHit;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
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
    public void SetCombatCamera(bool condition)
    {
        _normalCamera.gameObject.SetActive(!condition);
        _combatCamera.gameObject.SetActive(condition);
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
            : new Vector3(_combatCamera.transform.forward.x, body.position.y, _combatCamera.transform.transform.forward.z);
    }
}
