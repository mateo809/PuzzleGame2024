using UnityEngine;

public class Sc_WallMovment : MonoBehaviour
{
    [Header("AnimatorSettings")]
    [SerializeField] private Animator _animator;    
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetTrigger("Right");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetTrigger("Left");
        }
    }       
}


