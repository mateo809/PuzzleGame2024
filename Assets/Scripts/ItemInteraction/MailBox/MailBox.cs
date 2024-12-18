using UnityEngine;

public class MailBox : InteractableObject
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Sc_AudioSelection _audioSelection;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.MailboxKeyID)
        {
            Debug.Log("Open");
            _audioSelection.PlaySound(Sc_IDSFXManager.unlockedMailboxID);
            _animator.SetBool("Open", true);
            InventoryManager.Instance.RemoveItemFromID(IDManager.MailboxKeyID);
            Destroy(this);
        }
        else
        {
            _audioSelection.PlaySound(Sc_IDSFXManager.lockedMailboxID);
            Debug.Log("Pas la bonne chef");
        }

    }
}