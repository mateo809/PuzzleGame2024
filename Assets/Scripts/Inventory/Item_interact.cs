using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class InventoryItem
{
    public string itemName;
    public int itemID;
}
public class Item_interact : MonoBehaviour, IInteractable
{

    public InventoryManager inventoryManager;
  
    public void Interact()
    {
        Debug.Log("interacting");
        inventoryManager.AddItem(gameObject.name);
        Destroy(gameObject, 0.1f);
    }
}
