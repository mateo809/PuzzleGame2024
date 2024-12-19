using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Sc_WayPoints : MonoBehaviour
{
    public List<Transform> points;

    private void Awake()
    {
        points = GetComponentsInChildren<Transform>().ToList();
        points.Remove(transform);
    }
}