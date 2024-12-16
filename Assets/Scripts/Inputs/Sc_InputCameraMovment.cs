//using UnityEngine;
//using UnityEngine.InputSystem;

//public class Sc_InputCameraMovment : MonoBehaviour
//{
//    [SerializeField] private InputActionReference _moveCamera;
//    [SerializeField] private Sc_CameraMovement _scCameraMovement;
      
//    public void MoveCamera()
//    {
//        Vector2 direction = _moveCamera.action.ReadValue<Vector2>();        

//        if (direction == Vector2.right)
//        {
//            _scCameraMovement.NextWaypoint();
//        }
//        else if (direction == -Vector2.right)
//        {
//            _scCameraMovement.LastWaypoint();
//        }
//    }
//}