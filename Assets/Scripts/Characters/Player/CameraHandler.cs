using Unity.Cinemachine;

using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _normalCamera;
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
    public Vector3 CameraLookDirection(Transform body)
    {
        return body.position - new Vector3(_normalCamera.transform.position.x, body.position.y, _normalCamera.transform.position.z);
    }
}
