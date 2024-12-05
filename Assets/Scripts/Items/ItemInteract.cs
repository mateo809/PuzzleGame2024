using System;
using UnityEngine;


[RequireComponent(typeof(InventoryItem))]
public class ItemInteract : MonoBehaviour
{
    public InventoryManager inventoryManager;


    //Will be use to interact from the object to the scene
 /*   public void Interact()
    {
        inventoryManager.AddItem();
        Destroy(gameObject, 0.1f);
    }*/
}

