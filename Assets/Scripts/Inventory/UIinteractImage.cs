using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIinteractImage : MonoBehaviour
{
    public Dictionary<int, GameObject> itemsUI = new Dictionary<int, GameObject>();
    

    public GameObject ItemUIPrefab;

    public void AddItemUI(itemData data)
    {
        GameObject go = Instantiate(ItemUIPrefab, transform);
        go.GetComponent<LinkToInventory>().Init(data);
        itemsUI.Add(data.itemID, go);
    }

    public void SelectItemUI(int itemID)
    {
        InventoryManager.Instance.selectedItemID = itemID;
        print($"selected ID : {InventoryManager.Instance.selectedItemID}");
    }

    public void RemoveCurrentItemUI()
    {
        GameObject go = itemsUI[InventoryManager.Instance.selectedItemID];
        itemsUI.Remove(InventoryManager.Instance.selectedItemID);
        Destroy(go);
    }

    public void DeactivateCurrentItemBG()
    {
        itemsUI[InventoryManager.Instance.selectedItemID]
        .transform.GetChild(0).gameObject.SetActive(false);
    }

}
