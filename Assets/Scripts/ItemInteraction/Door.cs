using UnityEngine;

public class Door : InteractableObject
{
    public int RequiredKeyID; 
    public bool IsFinalDoor = false; 
    public int FinalKeyID; 

    private bool _isUnlocked = false;

    [SerializeField]
    private UIinteractImage uIinteractImage;


    public override void DoInteraction()
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
                //change condition
                if (InventoryManager.Instance.selectedItemID == RequiredKeyID)
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
    private bool CheckIfFinalDoorCanBeOpened()
    {
        if (InventoryManager.Instance.HasItem(FinalKeyID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
