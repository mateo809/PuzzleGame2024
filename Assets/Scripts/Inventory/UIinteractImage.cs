using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIinteractImage : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public KeyDoorManager keyDoorManager;

    [Header("Interactables Images")]
    [SerializeField] private RawImage[] interactableImages;
    [SerializeField] private string[] itemNames;

    void Update()
    {
        for (int i = 0; i < interactableImages.Length; i++)
        {
            UpdateInventoryImage(interactableImages[i], i);
        }
    }

    private void UpdateInventoryImage(RawImage InventoryImage, int itemID)
    {
        bool hasItem = inventoryManager.HasItem(itemID);
        InventoryImage.enabled = hasItem;
        
    }
}
