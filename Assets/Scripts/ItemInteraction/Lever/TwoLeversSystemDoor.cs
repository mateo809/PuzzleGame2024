using UnityEngine;

public class TwoLeversSystemDoor : MonoBehaviour
{
    private bool l1isActivated = false;
    private bool l2isActivated = false;


    private void CheckLever()
    {
        if (l1isActivated && l2isActivated)
        {
            Debug.Log("OpenDoor");
        }
    }

    public void ToggleLever(int leverIndex)
    {
        if(leverIndex == 1)
        {
            l1isActivated = !l1isActivated;
        }
        else if (leverIndex == 2)
        {
            l2isActivated = !l2isActivated;
        }

        CheckLever();
    }
}
