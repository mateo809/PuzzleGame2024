using System;
using UnityEngine;


public class InventoryItem : MonoBehaviour
{
    public itemData data;

    public InventoryItem()
    {
        data.itemID = -1;
        data.itemName = "Empty";
        data.sprite = null;
    }

    public InventoryItem(int itemID, string itemName, Sprite sprite)
    {
        this.data.itemID = itemID;
        this.data.itemName = itemName;
        this.data.sprite = sprite;

    }

    public InventoryItem(itemData dataToCopy)
    {
        this.data.itemID = dataToCopy.itemID;
        this.data.itemName = dataToCopy.itemName;
        this.data.sprite = dataToCopy.sprite;
    }
    
}
[Serializable]
public struct itemData
{
    public int itemID;
    public string itemName;
    public Sprite sprite;
}