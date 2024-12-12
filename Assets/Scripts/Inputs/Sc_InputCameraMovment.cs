using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_InputCameraMovment : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveCamera;
    [SerializeField] private Sc_CameraMovement _scCameraMovement;
    
    [Header("AnimatorSettings")]
    [SerializeField] private List<Animator> Walls = new List<Animator>();
       
    public void MoveCamera()
    {
        Vector2 direction = _moveCamera.action.ReadValue<Vector2>();        

        if (direction == Vector2.right && !_scCameraMovement._isTurningL && !_scCameraMovement._isTurningR)
        {
            _scCameraMovement.NextWaypoint();
            for (int i = 0; i < Walls.Count; i++)
            {
                Walls[i].SetTrigger("Right");
            }
        }
        else if (direction == -Vector2.right && !_scCameraMovement._isTurningR && !_scCameraMovement._isTurningL)
        {
            _scCameraMovement.LastWaypoint();
            for (int i = 0; i < Walls.Count; i++)
            {
                Walls[i].SetTrigger("Left");
            }
        }
    }
}