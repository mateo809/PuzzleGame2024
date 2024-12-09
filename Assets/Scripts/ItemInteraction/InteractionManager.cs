using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if(Camera.main == null)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, interactableMask))
        {

            InventoryItem item = hit.collider.gameObject.GetComponent<InventoryItem>();
            if (item != null)
            {
                print("item");
                if(InventoryManager.Instance.TryAddToInventory(item.data))
                {
                         
                }
                
                Destroy(item.gameObject);
            }
            else
            {
                if (hit.collider.gameObject.CompareTag("Door"))
                {
                    hit.collider.gameObject.GetComponent<Door>().DoInteraction();
                    
                    print("object");
                }
            }
        }
    }
}
