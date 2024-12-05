using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIinteractImage : MonoBehaviour
{
    public List<GameObject> itemsUI = new List<GameObject>();

    public GameObject ItemUIPrefab;

    public void AddItemUI(itemData data)
    {
        GameObject go = Instantiate(ItemUIPrefab, transform);
        go.GetComponent<LinkToInventory>().Init(data);
        itemsUI.Add(go);
    }

    public void SelectItemUI(int itemID)
    {
        InventoryManager.Instance.selectedItemID = itemID;
        print($"selected ID : {InventoryManager.Instance.selectedItemID}");
    }

}
