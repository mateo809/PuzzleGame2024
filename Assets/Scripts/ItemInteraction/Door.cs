using UnityEngine;

public class Door : MonoBehaviour
{
    public string RequiredKeyID; 
    public bool IsFinalDoor = false; 
    public string FinalKeyID; 

    private bool _isUnlocked = false;

    private void Update()
    {
        if (!_isUnlocked && Input.GetMouseButtonDown(0))
        {
            DetectDoor();
        }
    }

    private void DetectDoor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!_isUnlocked)
                    {
                        if (IsFinalDoor)
                        {
                            if (CheckIfFinalDoorCanBeOpened())
                            {
                                Debug.Log("Final Door Unlocked!");
                                _isUnlocked = true;
                            }
                            else
                            {
                                Debug.Log("You need to complete the game to open this door.");
                            }
                        }
                        else
                        {
                            if (KeyDoorManager.Instance.HasKey(RequiredKeyID))
                            {
                                Debug.Log("Open");
                                _isUnlocked = true;
                            }
                            else
                            {
                                Debug.Log("Pas la bonne chef");
                            }
                        }
                    }
                }
            }
        }
    }
    private bool CheckIfFinalDoorCanBeOpened()
    {
        if (KeyDoorManager.Instance.HasKey(FinalKeyID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
