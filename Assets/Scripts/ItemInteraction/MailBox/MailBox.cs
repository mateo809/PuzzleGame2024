using UnityEngine;

public class MailBox : InteractableObject
{
    [SerializeField]
    private UIinteractImage uIinteractImage;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.MailboxKeyID)
        {
            Debug.Log("Open");
            InventoryManager.Instance.RemoveItemFromID(IDManager.MailboxKeyID);
        }

        else
            return;
    }
}
