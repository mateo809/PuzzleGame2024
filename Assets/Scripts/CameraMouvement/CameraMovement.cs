using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    #region Camera Rotation
    [Header("   Camera Rotation")]
    [SerializeField] private float _cameraSpeed = 5;
    private float _speedMultiplier = 20;
    [SerializeField] private Transform _cameraPivotTrans;
    private float _currCameraStickRot = 0.0f;
    private float _nextCameraStickRot = 0.0f;

    private bool _isRotRight = false;
    private bool _isRotLeft = false;

    private IEnumerator RotateCameraPivot()
    {
        while (Mathf.Abs(_currCameraStickRot - ((360 + _nextCameraStickRot) % 360)) > 1f)
        {
            float rotationStep = _speedMultiplier * _cameraSpeed * Time.deltaTime * (_isRotLeft ? -1 : 1);

            _cameraPivotTrans.Rotate(Vector3.up, rotationStep);

            _currCameraStickRot = _cameraPivotTrans.eulerAngles.y;
            yield return null;
        }
        _currCameraStickRot = _nextCameraStickRot;
        _isRotLeft = false;
        _isRotRight = false;

        if (Mathf.Abs(_currCameraStickRot) >= 360.0f)
            _currCameraStickRot %= 360.0f;

        yield return null;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed && !(_isRotLeft || _isRotRight))
        {
            if (context.ReadValue<Vector2>().x == 0) return;

            _isRotLeft = context.ReadValue<Vector2>().x > 0;
            _isRotRight = !_isRotLeft;
            _nextCameraStickRot = (_nextCameraStickRot + (_isRotLeft ? -1 : 1) * 90.0f) % 360;
            StartCoroutine(RotateCameraPivot());
        }
    }
    #endregion
    #region Camera Focus

    [Header("   Camera Focus")]
    [SerializeField] private float _focusSpeed = 5.0f;
    [HideInInspector] public bool IsCameraFocused = false;
    private float _normalCameraSize;
    [SerializeField] private float _zoomedCameraSize;
    private float _currCameraZoom;

    void Start()
    {
        _normalCameraSize = Camera.main.orthographicSize;
        _currCameraZoom = _normalCameraSize;
    }

    private IEnumerator FocusOnTarget(Vector3 focusTarget, float targetCameraZoom)
    {
        _currCameraZoom = Camera.main.orthographicSize;

        float startingCameraSize = _currCameraZoom;
        Vector3 startingCameraPos = _cameraPivotTrans.position;
        float elapsedTime = 0.0f;
        while (Mathf.Abs(_currCameraZoom - targetCameraZoom) > 1f)
        {
            Debug.Log("Camera Ok");

            _currCameraZoom = Mathf.Lerp(startingCameraSize, targetCameraZoom, elapsedTime);
            Camera.main.orthographicSize = _currCameraZoom;

            _cameraPivotTrans.position = Vector3.Lerp(startingCameraPos, focusTarget, elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _currCameraZoom = targetCameraZoom;

        Debug.Log("Zoom Complete");

        yield return null;
    }

    public void Focus(Vector3 focusTarget)
    {
        StartCoroutine(FocusOnTarget(focusTarget, _zoomedCameraSize));
    }
    [ContextMenu("Debug/Focus")]
    public void Test()
    {
        Focus(Vector3.zero);
    }

    [ContextMenu("Debug/ResetFocus")]
    public void LoseFocus()
    {
        StartCoroutine(FocusOnTarget(Vector3.zero, _normalCameraSize));
    }

    #endregion
}
