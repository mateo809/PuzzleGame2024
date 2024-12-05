using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SearchService;



public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public UIinteractImage itemUIManager;
    [SerializeField]
    public List<itemData> inventory = new List<itemData>();
    [SerializeField]
    private int maxInventorySize = 4;
    public int selectedItemID = -1;

    private void Awake()
    {
        if(Instance == null)
        {          
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectItem();
        }
    }

    private void DetectItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Item"))
            {
/*                if(inventory.Count >= maxInventorySize)
                {
                    print("InventoryFULL");
                    return;
                }
                InventoryItem item = hit.collider.gameObject.GetComponent<InventoryItem>();
                if (item != null)
                {
                    inventory.Add(item.data);
                    itemUIManager.AddItemUI(item.data);
                    //Destroy(item.gameObject);
                }*/
            }
        }
    }

    public void SetSelectedItem(int itemID)
    {
        if(selectedItemID != -1)
            itemUIManager.itemsUI.Find(go => go.GetComponent<LinkToInventory>().data.itemID == selectedItemID)
                .transform.GetChild(0).gameObject.SetActive(false);
        if(selectedItemID != itemID)
        {
            selectedItemID = itemID;
        }
        else
        {
            selectedItemID = -1;
        }
            

    }

    public bool HasItem(int itemID)
    {
        return inventory.Exists(data => data.itemID == itemID);
            
    }

}
