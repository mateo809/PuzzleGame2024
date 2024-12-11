using TMPro;
using UnityEngine;

public class Door : InteractableObject
{

    public bool IsFinalDoor = false;
    [SerializeField] private TextMeshProUGUI _WinText;

    [SerializeField]
    private UIinteractImage uIinteractImage;



    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _interactionID)
        {
            Debug.Log("Open");
            InventoryManager.Instance.RemoveItemFromID(_interactionID);
            if (IsFinalDoor) {
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
