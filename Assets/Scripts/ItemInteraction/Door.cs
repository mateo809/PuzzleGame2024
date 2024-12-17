using TMPro;
using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private TextMeshProUGUI _WinText;

    [SerializeField] private UIinteractImage _UIInteractImage;

    [SerializeField] private DoorID _id;
    public enum DoorID
    {
        HouseDoor,
        ShedDoor
    }
    private void Start()
    {
        _interactionID = _id == DoorID.HouseDoor ? IDManager.FinalDoorKeyID : IDManager.ShedDoorKeyID;
    }

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _interactionID)
        {
            Debug.Log("Open");
            InventoryManager.Instance.RemoveItemFromID(_interactionID);
            if (_id == DoorID.HouseDoor) 
            {
                Debug.Log("Final Door Unlocked!");
            }
            Destroy(this);
        }
        else
        {
            Debug.Log("Pas la bonne chef");
        }
    }
}
