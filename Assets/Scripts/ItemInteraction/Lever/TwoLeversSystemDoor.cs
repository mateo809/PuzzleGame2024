using UnityEngine;
using System.Collections;

public class TwoLeversSystemDoor : MonoBehaviour
{
    public bool _l1isActivated = false;
    public bool _l2isActivated = false;
    //-2 for ID before init to avoid complications with the conditions using selectedItemID
    public int _l1ID = -2;
    public int _l2ID = -2;

    [SerializeField] private GameObject _mapGarden;
    [SerializeField] private GameObject _mapCave;

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
            Debug.Log("OpenDoor");
            _mapCave.gameObject.SetActive(true);
            _mapGarden.gameObject.SetActive(false); 
            //cave animator dans InpitCamera
        }
        else
        {
            Debug.Log("DoorIsClosed");
        }
    }

    public void ToggleLever(int leverIndex, bool wheightedActivation)
    {
        if(leverIndex == _l1ID)
        {
            _l1isActivated = true;
            print("L1activated");

        }
        else if (leverIndex == _l2ID)
        {
            _l2isActivated = true;
            print("L2activated");
        }

        CheckLever();
        if(!wheightedActivation)
            StartCoroutine(RevertLeverTimer(leverIndex));
    }

    private IEnumerator RevertLeverTimer(int leverIndex)
    {
        yield return new WaitForSeconds(1);

        if (leverIndex == _l1ID)
        {
            _l1isActivated = false;
        }
        else if (leverIndex == _l2ID)
        {
            _l2isActivated = false;
        }
        CheckLever();
    }
}
