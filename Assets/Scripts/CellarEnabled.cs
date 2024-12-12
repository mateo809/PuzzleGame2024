using System.Collections.Generic;
using UnityEngine;

public class CellarEnabled : MonoBehaviour
{
    [SerializeField] private Sc_CameraMovement sc_cam;
    [Header("AnimatorSettings")]
    [SerializeField] private Animator Walls;
    public void OnEnable()
    {
        if(sc_cam._waypointindex == 2) {
            Walls.SetTrigger("Right");
        }
    }
}
