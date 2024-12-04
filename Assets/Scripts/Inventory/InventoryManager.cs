using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;



public class InventoryManager : MonoBehaviour
{
    public HashSet<InventoryItem> inventory = new HashSet<InventoryItem>();

  
    public void AddItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemID == item.itemID);

        if (existingItem != null)
        {
            //existingItem.quantity++;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { itemName = itemName, itemID = 1 };
            inventory.Add(newItem);
        }
    }

    public bool HasItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);
        return existingItem != null && existingItem.itemID > 0;
    }

   
}
