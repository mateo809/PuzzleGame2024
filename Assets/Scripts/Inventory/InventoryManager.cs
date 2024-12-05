using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    [SerializeField]
    public List<itemData> inventory = new List<itemData>();

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
                InventoryItem item = hit.collider.gameObject.GetComponent<InventoryItem>();
                if (item != null)
                {
                    //AddItem(item);
                    inventory.Add(item.data);
                    //Destroy(item.gameObject);
                }
            }
        }
    }
    public void AddItem(InventoryItem item)
    {
        InventoryItem newItem = new InventoryItem(item.data);
        inventory.Add(newItem.data);
    }

    public bool HasItem(int itemID)
    {
        return inventory.Exists(data => data.itemID == itemID);
            
        //return data.itemID != -1;
            
    }

}
