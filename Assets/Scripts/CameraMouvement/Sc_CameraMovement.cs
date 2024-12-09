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

    private int _waypointindex = 0;

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
}
