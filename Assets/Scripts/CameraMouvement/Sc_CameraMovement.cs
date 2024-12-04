using UnityEngine;

public class Sc_CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetCenter;
    [SerializeField] private Sc_WayPoints _waypoints;

    private Transform _target;
    private int _waypointindex = 0;

    void Start()
    {
        _target = _waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        Vector3 dirCenter = _targetCenter.position - transform.position;
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dirCenter);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, _target.position) <= 0.5f)
        {
            NextWaypoint();
        }
    }

    public void NextWaypoint()
    {
        if (_waypointindex >= _waypoints.points.Count - 1)
        {
            _waypointindex = 0;
            return;
        }

        _waypointindex++;
        _target = _waypoints.points[_waypointindex];
    }

    public void GoRight()
    {

    }
}
