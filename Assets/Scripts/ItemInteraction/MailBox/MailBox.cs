using UnityEngine;

public class MailBox : InteractableObject
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Sc_AudioSelection _audioSelection;
    [SerializeField] private GameObject _ColliderPaper;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.MailboxKeyID)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            _ColliderPaper.GetComponent<Collider>().enabled = true;  
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