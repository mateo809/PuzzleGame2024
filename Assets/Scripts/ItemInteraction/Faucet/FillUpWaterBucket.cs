using UnityEngine;

public class FillUpWaterBucket : InteractableObject
{
    [SerializeField] private itemData waterBucketData;


    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.WaterBucketID) //Check water bucket
        {
            InventoryManager.Instance.RemoveItemFromID(IDManager.WaterBucketID);
            InventoryManager.Instance.TryAddToInventory(waterBucketData);
        }
    }
}
