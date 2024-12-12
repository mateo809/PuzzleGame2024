using UnityEngine;

public class FillUpWaterBucket : InteractableObject
{
    [SerializeField] private itemData waterBucketData;


    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.EmptyBucketID) //Check for EmptyWaterBucket
        {
            InventoryManager.Instance.RemoveItemFromID(IDManager.EmptyBucketID);
            InventoryManager.Instance.TryAddToInventory(waterBucketData);
        }
    }
}
