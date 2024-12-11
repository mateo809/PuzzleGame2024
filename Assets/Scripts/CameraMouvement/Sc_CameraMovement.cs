using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_CameraMovement : MonoBehaviour
{

    //Camera 
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetCenter;
    [SerializeField] private Sc_WayPoints _waypoints;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private GameObject _boomStick;
    [SerializeField] private Camera _camera;

    //Rotation
    private List<Quaternion> _target = new List<Quaternion>();
    [SerializeField] private bool _isTurningL;
    [SerializeField] private bool _isTurningR;

    [SerializeField] private float _duration;
    private bool _isZooming;
    private float _zoomTarget;
    private Vector3 _zoomTargetPos;
    private int _waypointindex = 0;

    [SerializeField] private List<GameObject> _steps = new List<GameObject>(3) { null, null, null };

    enum GameObjectSize
    {
        Far = 20,
        Medium = 10,
        Near = 5,
    }

    void Start()
    {
        _target.Add(new Quaternion(0, 0, 0, 1));
        _target.Add(new Quaternion(0, 0.707106829f, 0, 0.707106829f));
        _target.Add(new Quaternion(0, -1, 0, 0));
        _target.Add(new Quaternion(0, -0.707106829f, 0, 0.707106829f));
    }

    private void Update()
    {
        if (_isTurningL)
        {
            GoRight();
        }

        if (_isTurningR)
        {
            GoLeft();
        }

        if (_isZooming)
        {
            CameraZoom(_zoomTarget, _zoomTargetPos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            DetectWayPoint();
        }
        if(Input.GetMouseButtonDown(1))
            GoBack();
    }

    public void NextWaypoint()
    {
        if (_isTurningR) return;
        _waypointindex--;
        if (_waypointindex < 0)
        {
            _waypointindex = _target.Count - 1;
        }
        _waypointindex %= _target.Count;
        GoRight();
    }

    public void GoRight()
    {
        _isTurningR = true;
        Debug.Log(_target[_waypointindex]);
        _targetCenter.transform.localRotation = Quaternion.Lerp(_targetCenter.transform.localRotation, _target[_waypointindex], Time.deltaTime * _speed);

        if (Quaternion.Angle(_targetCenter.transform.localRotation, _target[_waypointindex]) < 0.1f)
        {
            _isTurningR = false;
        }
    }

    public void LastWaypoint()
    {
        if (_isTurningR) return;
        _waypointindex++;
        _waypointindex %= _target.Count;
        GoLeft();
    }

    public void GoLeft()
    {
        _isTurningR = true;
        Debug.Log(_target[_waypointindex]);
        _targetCenter.transform.localRotation = Quaternion.Lerp(_targetCenter.transform.localRotation, _target[_waypointindex], Time.deltaTime * _speed);

        if (Quaternion.Angle(_targetCenter.transform.localRotation, _target[_waypointindex]) < 0.1f)
        {
            _isTurningR = false;
        }
    }

    public void CameraZoom(float p_zoomAmnt, Vector3 p_zoomPos)
    {
        _zoomTarget = p_zoomAmnt;
        _zoomTargetPos = p_zoomPos;
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, p_zoomAmnt, Time.deltaTime / _duration);
        _boomStick.transform.position = Vector3.Lerp(_boomStick.transform.position, p_zoomPos, Time.deltaTime / _duration);

        if (Math.Abs(p_zoomAmnt - _camera.orthographicSize) < 0.01f && Vector3.Distance(_boomStick.transform.position, p_zoomPos) < 0.01f )
        {
            _isZooming = false;
        }
    }

    public void GoBack()
    {
        Debug.Log("Go back start");
        switch (Mathf.RoundToInt(_camera.orthographicSize))
        {
            case (int)GameObjectSize.Near:
                if (_steps[0] == null) break;

                Debug.Log("near");

                _steps[0].GetComponent<Collider>().enabled = true;

                if (_steps[1] == null)
                {
                    if (_steps[2] == null)
                    {
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Far, Vector3.zero);

                    }
                    else
                    {
                        _steps[2].GetComponent<Collider>().enabled = false;
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Far, _steps[2].transform.position);
                    }

                }
                else
                {
                    _steps[1].GetComponent<Collider>().enabled = false;
                    _isZooming = true;
                    CameraZoom((int)GameObjectSize.Medium, _steps[1].transform.position);
                }
                _steps[0] = null;
                break;

            case (int)GameObjectSize.Medium:

                Debug.Log("medium");

                if (_steps[1] == null)
                {
                    break;
                }
                _steps[1].GetComponent<Collider>().enabled = true;

                if (_steps[2] == null)
                {
                    _isZooming = true;
                    CameraZoom((int)GameObjectSize.Far, Vector3.zero);
                }
                else
                {
                    _steps[2].GetComponent<Collider>().enabled = true;
                    _isZooming = true;
                    CameraZoom((int)GameObjectSize.Far, _steps[2].transform.position);
                }
                _steps[1] = null;
                break;

            case (int)GameObjectSize.Far:

                Debug.Log("far");

                if (_steps[2] == null) break;

                _steps[2].GetComponent<Collider>().enabled = true;
                _isZooming = true;
                CameraZoom((int)GameObjectSize.Far,Vector3.zero);

                _steps[2] = null;
                break;
        }
    }

    private void DetectWayPoint()
    {
        if (!_camera.gameObject.activeSelf)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPos;
            Debug.Log("ray hit");

            if (hit.collider.gameObject.GetComponent<ItemWaypoint>() != null && !hit.collider.gameObject.GetComponent<ItemWaypoint>().canBePreviewed)
            {
                switch (hit.collider.gameObject.GetComponent<ItemWaypoint>().itemSize)
                {
                    case 1:
                    Debug.Log("Waypoint small reached");
                        targetPos = hit.transform.position;
                        _steps[0] = hit.collider.gameObject;
                        hit.collider.enabled = false;
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Near, targetPos);
                        break;

                    case 2:
                        targetPos = hit.transform.position;
                        _steps[1] = hit.collider.gameObject;
                        hit.collider.enabled = false;
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Medium, targetPos);
                        if (_steps[0] != null)
                        {
                            _steps[0].GetComponent<Collider>().enabled = true;
                        }
                        _steps[0] = null;
                        Debug.Log("Waypoint medium reached");
                        break;

                    case 3:
                        targetPos = hit.transform.position;
                        _steps[2] = hit.collider.gameObject;
                        hit.collider.enabled = false;
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Far, targetPos);
                        if (_steps[1] != null)
                        {
                            _steps[1].GetComponent<Collider>().enabled = true;
                        }
                        _steps[1] = null;
                        if (_steps[0] != null)
                        {
                            _steps[0].GetComponent<Collider>().enabled = true;
                        }
                        _steps[0] = null;
                        Debug.Log("Waypoint large reached");
                        break;
                }
            }
        }
    }
}
