using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CodeLocker : MonoBehaviour
{
    [SerializeField] private List<Transform> _wheelList = new List<Transform>();
    [SerializeField] private List<int> _correctCode = new List<int>();
    private List<int> _currentCode = new List<int>();
    [SerializeField] private Animator _animator;
    [SerializeField] private Camera _interactionCamera; 
    private Camera _currentCamera; 
    private Camera _originalCamera;
    [SerializeField] private Collider _codeLockerCollider; 

    private void Start()
    {
        for (int i = 0; i < _wheelList.Count; i++)
        {
            _currentCode.Add(0);
        }

        if (_codeLockerCollider == null)
        {
            _codeLockerCollider = GetComponent<Collider>(); 
        }

        _originalCamera = Camera.main; 
        _currentCamera = _originalCamera; 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectClickedWheel();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ExitCameraView();
        }
    }

    private void DetectClickedWheel()
    {
        if (_currentCamera == null) return; 

        Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction.normalized * 1000, Color.red, 1);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("CodeLocker"))
            {
                if (_codeLockerCollider != null)
                {
                    _codeLockerCollider.enabled = false; 
                }
                SwitchToCamera(_interactionCamera); 
                return;
            }

            int wheelIndex = _wheelList.FindIndex(wheel => wheel == hit.transform);

            if (wheelIndex != -1)
            {
                RotateWheel(hit.transform, wheelIndex);
            }
        }
    }

    private void RotateWheel(Transform wheel, int index)
    {
        wheel.Rotate(wheel.forward, 36, Space.World);
        _currentCode[index]++;
        if (_currentCode[index] == 10) _currentCode[index] = 0;
        CheckCode();
    }

    private void CheckCode()
    {
        if (_currentCode.SequenceEqual(_correctCode)) 
        {
            Debug.Log("Open");
            Destroy(gameObject);
           ExitCameraView();
            _currentCamera = _originalCamera;
            _animator.SetBool("Open", true);
        }
    }

    public void SwitchToCamera(Camera newCamera)
    {
        if (newCamera == null) return;
        if (_currentCamera != null)
        {
            _currentCamera.gameObject.SetActive(false); 
        }

        _currentCamera = newCamera; 
        _currentCamera.gameObject.SetActive(true); 
    }

    public void ExitCameraView()
    {
        if (_originalCamera != null)
        {
            _currentCamera.gameObject.SetActive(false); 
            _currentCamera = _originalCamera; 
            _currentCamera.gameObject.SetActive(true);

            if (_codeLockerCollider != null)
            {
                _codeLockerCollider.enabled = true; 
            }
        }
    }
}
