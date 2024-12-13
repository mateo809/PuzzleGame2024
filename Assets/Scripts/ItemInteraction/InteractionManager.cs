using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableMask;

    [SerializeField, Tooltip("Speed multiplier for movement interactions.")]
    private float movementSpeed = 0.25f; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (Camera.main == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction.normalized * 1000, Color.red, 10);

        if (Physics.Raycast(ray, out hit, 1000, interactableMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            InventoryItem item = hitObject.GetComponent<InventoryItem>();
            if (item != null)
            {
                print("item");
                if (InventoryManager.Instance.TryAddToInventory(item.data))
                {
                    Destroy(hitObject);
                }
            }
            else
            {
                string tag = hitObject.tag;

                if (hitObject.GetComponent<InteractableObject>() != null)
                {
                    hitObject.GetComponent<InteractableObject>().DoInteraction();
                }
                else
                {
                    switch (tag)
                    {
                        case "MovementXPlus":
                        case "MovementXMinus":
                        case "MovementZPlus":
                        case "MovementZMinus":
                            HandleMovement(hitObject, tag);
                            break;
                        default:
                            throw new System.Exception($"Tag {tag} was not recognized");
                    }
                }
            }
        }
    }

    private void HandleMovement(GameObject obj, string tag)
    {
        Vector3 offset = Vector3.zero;
        string newTag = tag;

        switch (tag)
        {
            case "MovementXPlus":
                offset = new Vector3(movementSpeed, 0, 0);
                newTag = "MovementXMinus";
                break;
            case "MovementXMinus":
                offset = new Vector3(-movementSpeed, 0, 0);
                newTag = "MovementXPlus";
                break;
            case "MovementZPlus":
                offset = new Vector3(0, 0, movementSpeed);
                newTag = "MovementZMinus";
                break;
            case "MovementZMinus":
                offset = new Vector3(0, 0, -movementSpeed);
                newTag = "MovementZPlus";
                break;
        }

        Vector3 newPosition = obj.transform.position + offset;
        obj.transform.position = newPosition;
        obj.tag = newTag;
    }
}
