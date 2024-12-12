using System.Collections;
using UnityEngine;

public class DiggingArea : InteractableObject
{
    [SerializeField] private itemData _Paper3Data;
    [SerializeField] private Animator _Animator;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.ShovelID) 
        {
            StartCoroutine(AnimShovel());
        }
    }

    private IEnumerator AnimShovel()
    {
        _Animator.SetBool("Dig", true);
        yield return new WaitForSeconds(2f);
        _Animator.SetBool("Dig", false);
        InventoryManager.Instance.RemoveItemFromID(IDManager.ShovelID);
        InventoryManager.Instance.TryAddToInventory(_Paper3Data);
        Destroy(gameObject);
    }
}