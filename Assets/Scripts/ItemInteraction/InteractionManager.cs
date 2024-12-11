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

        Debug.DrawRay(ray.origin, ray.direction.normalized * 1000, Color.red,10);

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
                string tag = hit.collider.gameObject.tag;

                switch (tag)
                {
                    case "Door":
                        hit.collider.gameObject.GetComponent<Door>().DoInteraction();
                        break;
                    case "Lever":
                        hit.collider.gameObject.GetComponent<Lever>().DoInteraction();
                        break;
                    case "Faucet":
                        hit.collider.gameObject.GetComponent<Faucet>().DoInteraction();
                        break;
                    case "DiggingArea":
                        hit.collider.gameObject.GetComponent<Diggingarea>().DoInteraction();
                        break;
                    case "Ladder":
                        hit.collider.gameObject.GetComponent<Ladder>().DoInteraction();
                        break;
                    default: throw new System.Exception($"Tag {tag} was not recognized");

                }
            }
        }
    }
}
