using UnityEngine;

public class Sc_DoormatMoving : MonoBehaviour
{
    [SerializeField] private Animator _swipDoormatAnimation;

    private void OnMouseDown()
    {
        Debug.Log("dd");
        _swipDoormatAnimation.SetTrigger("Touch");
    }
}
