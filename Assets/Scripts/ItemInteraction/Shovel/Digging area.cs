using System.Collections;
using UnityEngine;

public class Diggingarea : InteractableObject
{
    [SerializeField] private int _bucketID;
    [SerializeField] private itemData _Paper;
    [SerializeField] private Animator _Animator;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _bucketID) 
        {
            StartCoroutine(AnimShovel());
        }
    }

    private IEnumerator AnimShovel()
    {
        _Animator.SetBool("Dig", true);
        yield return new WaitForSeconds(2f);
        _Animator.SetBool("Dig", false);
        InventoryManager.Instance.RemoveItemFromID(_bucketID);
        InventoryManager.Instance.TryAddToInventory(_Paper);
        Destroy(gameObject);
    }
}