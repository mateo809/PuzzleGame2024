using UnityEngine;
using System.Collections.Generic;

public class Sc_CameraZoomHelper : MonoBehaviour
{
    [SerializeField] private List<Collider> zoomTargets = new List<Collider>();

    public void SetZoomTargetEnabled(bool value)
    {
        foreach (Collider coll in zoomTargets)
        {
            coll.enabled = value;
        }
    }
}