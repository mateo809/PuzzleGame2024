using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SearchService;
using static UnityEditor.Progress;



public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public UIinteractImage itemUIManager;
    [SerializeField]
    public List<itemData> inventory = new List<itemData>();
    [SerializeField]
    public int maxInventorySize = 4;
    public int selectedItemID = -1;

    private void Awake()
    {
        if(Instance == null)
        {          
            Instance = this;
        }
    }


    public bool TryAddToInventory(itemData item)
    {

        if (inventory.Count >= maxInventorySize)
        {
            print("InventoryFULL");
            return false;
        }
        else
        {
            inventory.Add(item);
            itemUIManager.AddItemUI(item);
            return true;
        }

    }

    public void SetSelectedItem(int itemID)
    {
        if(selectedItemID != -1)
            itemUIManager.DeactivateCurrentItemBG();

        selectedItemID = selectedItemID != itemID ? itemID : -1;
    }

    public void RemoveCurrItem(int itemID)
    {
        if (!HasItem(itemID))
            return;

        inventory.Remove(inventory.FirstOrDefault(item => item.itemID == itemID));
        itemUIManager.RemoveCurrentItemUI();
        selectedItemID = -1;
    }

    public bool HasItem(int itemID)
    {
        return inventory.Exists(data => data.itemID == itemID);
            
    }

}
