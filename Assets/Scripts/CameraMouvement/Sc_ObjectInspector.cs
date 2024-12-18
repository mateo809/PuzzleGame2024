using System;
using UnityEngine;

public class Sc_ObjectInspector : MonoBehaviour
{
    [SerializeField] private Transform _inspectionTarget;

    [Serializable]
    public struct CameraParams
    {
        public bool isOrtho;
        public float FOV;
        public Vector3 position;
        public Quaternion rotation;
    }

    private CameraParams _mainCameraParams;
    private CameraParams _inspectionCameraParams = new CameraParams { isOrtho = false, FOV = 22.0f };

    // private void Start()
    // {
    //     _mainCameraParams.isOrtho = Camera.main.orthographic;
    //     _mainCameraParams.FOV = Camera.main.orthographicSize;
    // }

    public void EnterInspectionMode()
    {
        if (_inspectionTarget == null) return;

        _mainCameraParams.isOrtho = Camera.main.orthographic;
        _mainCameraParams.FOV = Camera.main.orthographicSize;
        _mainCameraParams.position = Camera.main.transform.position;
        _mainCameraParams.rotation = Camera.main.transform.rotation;

        Camera.main.orthographic = _inspectionCameraParams.isOrtho;
        Camera.main.orthographicSize = _inspectionCameraParams.FOV;
        Camera.main.transform.position = _inspectionTarget.position;
        Camera.main.transform.rotation = _inspectionTarget.rotation;
    }

    public void ExitInspectionMode()
    {
        Camera.main.orthographic = _mainCameraParams.isOrtho;
        Camera.main.orthographicSize = _mainCameraParams.FOV;
        Camera.main.transform.position = _mainCameraParams.position;
        Camera.main.transform.rotation = _mainCameraParams.rotation;
    }
}
