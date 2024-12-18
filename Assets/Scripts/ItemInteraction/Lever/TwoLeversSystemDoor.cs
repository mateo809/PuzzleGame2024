using UnityEngine;


public class TwoLeversSystemDoor : MonoBehaviour
{
    private bool _l1isActivated = false;
    private bool _l2isActivated = false;
    //-2 for ID before init to avoid complications with the conditions using selectedItemID
    private int _l1ID = -2;
    private int _l2ID = -2;

    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _lever1Animator;
    [SerializeField] private Animator _lever2Animator;

    [SerializeField] private Sc_ExitMap _exitMap;

    public void SetLeverID(int leverID)
    {
        if(_l1ID != leverID && _l2ID != leverID)
        {
            if(_l1ID == -2) _l1ID = leverID;
            else if(_l2ID == -2) _l2ID = leverID;
        }
    }

    private void CheckLever()
    {
        if (_l1isActivated && _l2isActivated)
        {
            Debug.Log("Door is open");
            _anim.SetBool("Open" , true);
            _exitMap.isUnlocked = true;
        }
        else
        {
            Debug.Log("Door is closed");
        }
    }

    public void ToggleLever(int leverID)
    {
        if(leverID == _l1ID)
        {
            _l1isActivated = !_l1isActivated;
            print("L1activated");
            ToggleLeverAnimation(_lever1Animator, _l1isActivated);

        }
        else if (leverID == _l2ID)
        {
            _l2isActivated = !_l2isActivated;
            print("L2activated");
            ToggleLeverAnimation(_lever2Animator, _l2isActivated);
        }

        CheckLever();          
    }

    private void ToggleLeverAnimation(Animator leverAnimator, bool isActivated)
    {
        if (leverAnimator != null)
        {
            leverAnimator.SetBool("IsActivated", isActivated);
        }
        else
        {
            Debug.LogWarning("Animator non assigné pour ce levier.");
        }
    }
}
