using UnityEngine;

public class Sc_ObjectInspector : MonoBehaviour
{
    [SerializeField] private Camera _inspectionCamera;      
    [SerializeField] private Transform _inspectionTarget;  
    private Camera _originalCamera;
    private Camera _currentCamera;
    private Vector3 _originalCameraPosition;                  
    private Quaternion _originalCameraRotation;              
    private bool _isInspecting = false;

    private void Start()
    {
        _originalCamera = Camera.main;
        if (_originalCamera != null)
        {
            _originalCameraPosition = _originalCamera.transform.position;
            _originalCameraRotation = _originalCamera.transform.rotation;
        }
        if (_inspectionCamera != null)
        {
            _inspectionCamera.gameObject.SetActive(false); 
        }
    }

    public void EnterInspectionMode()
    {
        if (_inspectionCamera == null || _inspectionTarget == null || _originalCamera == null) return;

        _originalCameraPosition = _originalCamera.transform.position;
        _originalCameraRotation = _originalCamera.transform.rotation;
        _originalCamera = Camera.main;

        _originalCamera.gameObject.SetActive(false);
        _inspectionCamera.gameObject.SetActive(true);

        _inspectionCamera.transform.position = _inspectionTarget.position;
        _inspectionCamera.transform.rotation = _inspectionTarget.rotation;

        _isInspecting = true;
    }

    public void ExitInspectionMode()
    {
        if (_originalCamera == null) return;

        _inspectionCamera.gameObject.SetActive(false);
        _originalCamera.gameObject.SetActive(true);

        _originalCamera.transform.position = _originalCameraPosition;
        _originalCamera.transform.rotation = _originalCameraRotation;

        _isInspecting = false;
    }

    //private void Update()
    //{
    //    if (_isInspecting && Input.GetMouseButtonDown(1))
    //    {
    //        ExitInspectionMode();
    //    }
    //}
}
