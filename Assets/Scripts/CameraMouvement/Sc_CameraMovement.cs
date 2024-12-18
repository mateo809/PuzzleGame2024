using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_CameraMovement : MonoBehaviour
{
    [Header("   General Parameters")]
    [SerializeField] private Transform _cameraPivotTrans;
    public bool IsInAction = false;

    [Header("   Animator Parameters")]
    [SerializeField] private List<Animator> _mapMoveWalls = new List<Animator>();
    private string _rotateDirection = string.Empty;

    #region Camera Rotation
    [Header("   Camera Rotation")]
    [SerializeField] private float _cameraSpeed = 5;
    private float _speedMultiplier = 20;
    private float _currCameraStickRot = 0.0f;
    private float _nextCameraStickRot = 0.0f;

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

        if (Mathf.Abs(_currCameraStickRot) >= 360.0f)
            _currCameraStickRot %= 360.0f;

        IsInAction = false;
        yield return null;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!IsInAction && context.performed)
        {
            if (context.ReadValue<Vector2>().x == 0) return;

            IsInAction = true;
            _isRotLeft = context.ReadValue<Vector2>().x > 0;     
            
            _rotateDirection = _isRotLeft ? "Right" : "Left";
           _mapMoveWalls[0].SetTrigger(_rotateDirection);
           _mapMoveWalls[1].SetTrigger(_rotateDirection);

            _nextCameraStickRot = (_nextCameraStickRot + (_isRotLeft ? -1 : 1) * 90.0f) % 360;
            StartCoroutine(RotateCameraPivot());
        }
    }
    #endregion

    #region Camera Focus

    [Header("   Camera Focus")]
    [SerializeField] private float _zoomAnimTimer = 2.0f;
    [HideInInspector] public bool IsCameraFocused = false;
    private float _normalCameraSize;
    [SerializeField] private float _zoomedCameraSize;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private Sc_CameraZoomHelper _cameraZoomHelper;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        _normalCameraSize = _mainCamera.orthographicSize;
    }

    private IEnumerator FocusOnTarget(Vector3 focusTarget, float targetCameraZoom)
    {
        float startingCameraSize = _mainCamera.orthographicSize;
        Vector3 startingCameraPos = _cameraPivotTrans.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < _zoomAnimTimer)
        {
            float zoomProgress = Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / _zoomAnimTimer);

            _mainCamera.orthographicSize = Mathf.Lerp(startingCameraSize, targetCameraZoom, zoomProgress);

            _cameraPivotTrans.position = Vector3.Lerp(startingCameraPos, focusTarget, zoomProgress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _mainCamera.orthographicSize = targetCameraZoom;
        _cameraPivotTrans.position = focusTarget;

        Debug.Log("Zoom Complete");

        IsInAction = false;
        yield return null;
    }

    public void Focus(Vector3 focusTarget)
    {
        if (IsInAction) return;

        IsInAction = true;

        IsCameraFocused = true;
        _backButton.SetActive(true);
        StartCoroutine(FocusOnTarget(focusTarget, _zoomedCameraSize));
        _cameraZoomHelper.SetZoomTargetEnabled(false);
    }

    public void LoseFocus()
    {
        if (IsInAction) return;

        IsInAction = true;

        IsCameraFocused = false;
        _backButton.SetActive(false);
        StartCoroutine(FocusOnTarget(Vector3.zero, _normalCameraSize));
        _cameraZoomHelper.SetZoomTargetEnabled(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LoseFocus();
        }
    }
    #endregion
}
