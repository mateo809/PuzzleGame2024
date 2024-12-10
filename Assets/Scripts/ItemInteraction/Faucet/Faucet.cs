using Unity.VisualScripting;
using UnityEngine;

public class Faucet : InteractableObject
{
    [SerializeField] private int _bucketID;
    [SerializeField] private itemData waterBucketData;


    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _bucketID) //Check water bucket
        {
            InventoryManager.Instance.RemoveItemFromID(_bucketID);
            InventoryManager.Instance.TryAddToInventory(waterBucketData);
        }
    }
}
