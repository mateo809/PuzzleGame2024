using UnityEngine;

public class Shed : InteractableObject
{
    [SerializeField]
    private UIinteractImage uIinteractImage;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _interactionID)
        {
            Debug.Log("Open");
            InventoryManager.Instance.RemoveItemFromID(_interactionID);
        }

        else
            return;
    }
}