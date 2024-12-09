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
    //Zoom
    [SerializeField] private bool _isZooming;
    [SerializeField] private float _duration;

    private float _zoomTarget;
    private int _waypointindex = 0;

    [SerializeField] private List<GameObject> _steps = new List<GameObject>(3){ null, null, null };

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
            CameraZoom(_zoomTarget);
        }

        if (Input.GetMouseButtonDown(0))
        {
            DetectWayPoint();
        }
    }

    public void NextWaypoint()
    {
        if (_isTurningR) return;
        _waypointindex--;
        if(_waypointindex < 0)
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
        _targetCenter.transform.localRotation = Quaternion.Lerp(_targetCenter.transform.localRotation, _target[_waypointindex],Time.deltaTime * _speed);

        if (Quaternion.Angle(_targetCenter.transform.localRotation,_target[_waypointindex]) < 0.1f)
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

    public void CameraZoom(float p_zoomAmnt)
    {
        _zoomTarget = p_zoomAmnt;
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, p_zoomAmnt, Time.deltaTime / _duration);

        if (Math.Abs(p_zoomAmnt - _camera.orthographicSize) < 0.1f)
        {
            _isZooming = false;
        }
    }

    public void GoBack()
    {
        switch (_camera.orthographicSize)
        {
            case (int)GameObjectSize.Near:

                _steps[0].GetComponent<BoxCollider>().enabled = true;
                if (_steps[1] == null)
                {
                    if (_steps[2]!= null)
                    {
                        _boomStick.transform.position = _steps[2].transform.position;
                        _steps[2].GetComponent<BoxCollider>().enabled = false;
                        _steps[2] = null;
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Far);
                    }
                    else
                    {
                        _boomStick.transform.position = Vector3.zero;
                        _steps[1].GetComponent<BoxCollider>().enabled = true;
                        _isZooming = true;
                        CameraZoom((int)GameObjectSize.Far);
                    }

                }
                else
                {
                    _boomStick.transform.position = _steps[1].transform.position;
                    _steps[1].GetComponent<BoxCollider>().enabled = false;
                    _steps[1] = null;
                    _isZooming = true;
                    CameraZoom((int)GameObjectSize.Medium);
                }
                break;

            case (int)GameObjectSize.Medium:

                _steps[1].GetComponent<BoxCollider>().enabled = true;
                if (_steps[2] == null)
                {
                    _boomStick.transform.position = Vector3.zero;
                    _steps[2].GetComponent<BoxCollider>().enabled = true;
                    _isZooming = true;
                    CameraZoom((int)GameObjectSize.Far);
                }
                else
                {
                    _boomStick.transform.position = _steps[2].transform.position;
                    _isZooming = true;
                    CameraZoom((int)GameObjectSize.Far);
                }
                _steps[2].GetComponent<BoxCollider>().enabled = false;
                _steps[2] = null;
                _isZooming = true;
                CameraZoom((int)GameObjectSize.Far);
                break;

            case (int)GameObjectSize.Far:

                _steps[2].GetComponent<BoxCollider>().enabled = true;
                _boomStick.transform.position = Vector3.zero;
                _steps[2] = null;
                _isZooming = true;
                CameraZoom((int)GameObjectSize.Far);
                break;
        }
    }

    private void DetectWayPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPos;
            Debug.Log("ray hit");
            switch (hit.collider.gameObject.tag)
            {
                case "WaypointSmall":

                    targetPos = hit.transform.position;
                    _boomStick.transform.position = targetPos;
                    _camera.orthographicSize = (int)GameObjectSize.Near;
                    Debug.Log("Waypoint small reached");
                    _steps[0] = hit.collider.gameObject;
                    break; 

                case "WaypointMedium":
                    targetPos = hit.transform.position;
                    _boomStick.transform.position = targetPos;
                    _camera.orthographicSize = (int) GameObjectSize.Medium;
                    _steps[1] = hit.collider.gameObject;
                    if (_steps[0] != null)
                    {
                        _steps[0].GetComponent<BoxCollider>().enabled = false;
                    }
                    _steps[0] = null;
                    Debug.Log("Waypoint medium reached");
                    break;

                case "WaypointLarge":

                    targetPos = hit.transform.position;
                    _boomStick.transform.position = targetPos;
                    _camera.orthographicSize = (int)GameObjectSize.Far;
                    _steps[2] = hit.collider.gameObject;
                    if (_steps[1] != null)
                    {
                        _steps[1].GetComponent<BoxCollider>().enabled = false;
                    }
                    _steps[1] = null;
                    if (_steps[0] != null)
                    {
                        _steps[0].GetComponent<BoxCollider>().enabled = false;
                    }
                    _steps[0] = null;
                    Debug.Log("Waypoint large reached");
                    break;
            }
            //if (hit.collider.gameObject.CompareTag("WaypointMedium"))
            //{
            //    Vector3 targetPos = hit.transform.position;
            //    _boomStick.transform.position = targetPos;
            //    _camera.orthographicSize = (int)GameObjectSize.Medium;
            //}

            //if (hit.collider.gameObject.CompareTag("WaypointSmall"))
            //{
            //    Vector3 targetPos = hit.transform.position;
            //    _boomStick.transform.position = targetPos;
            //    _camera.orthographicSize = (int)GameObjectSize.Near;
            //}
        }
    }
}