using UnityEngine;

public class MailBox : InteractableObject
{
    [SerializeField] private Animator _animator;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.MailboxKeyID)
        {
            Debug.Log("Open");
            _animator.SetBool("Open", true);
            InventoryManager.Instance.RemoveItemFromID(IDManager.MailboxKeyID);
            Destroy(this);
        }
        else
        {
            Debug.Log("Pas la bonne chef");
        }

    }
}