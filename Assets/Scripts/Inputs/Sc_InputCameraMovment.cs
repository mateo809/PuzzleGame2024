using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_InputCameraMovment : MonoBehaviour
{   
    private InputAction _moveCamera;
    [SerializeField] private Sc_CameraMovement _scCameraMovement;

    private void Start()
    {      
        _moveCamera = GetComponent<PlayerInput>().actions.FindAction("CameraMovment");
    }   
    public void MoveCamera()
    {
        Vector2 direction = _moveCamera.ReadValue<Vector2>();
        if (direction == Vector2.right)
        {
            _scCameraMovement.NextWaypoint();            
        }
        else if (direction == - Vector2.right)
        {
            _scCameraMovement.LastWaypoint();
        }
    }
}
