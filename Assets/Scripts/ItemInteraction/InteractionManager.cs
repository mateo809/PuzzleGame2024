using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private GameObject cameraControllerObject;

    [SerializeField, Tooltip("Speed multiplier for movement interactions.")]
    private float movementSpeed = 0.25f;

    private Sc_CameraMovement _cameraMover;
    private Camera _currentCamera;

    private Sc_ObjectInspector inspectedElement = null;

    // private void Start()
    // {
    //     _currentCamera = Camera.main;

    //     if (cameraControllerObject != null)
    //     {
    //         _cameraMover = cameraControllerObject.GetComponent<Sc_CameraMovement>();
    //     }
    //     else
    //     {
    //         Debug.LogError("Camera Controller Object is not assigned!");
    //     }
    // }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Interact();
    //     }
    //     if (Input.GetMouseButtonDown(1) && inspectedElement != null)
    //     {
    //         ExitInspectionMode();
    //     }
    // }

    private void Interact()
    {
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

                if (_cameraMover != null && !_cameraMover.IsCameraFocused && tag == "ZoomTarget")
                {
                    _cameraMover.Focus(hit.collider.gameObject.transform.localToWorldMatrix.GetPosition());
                }
                else if (hitObject.GetComponent<InteractableObject>() != null)
                {
                    hitObject.GetComponent<InteractableObject>().DoInteraction();
                }
                else
                {
                    switch (tag)
                    {
                        case "RotateY":
                        case "MovementXPlus":
                        case "MovementXMinus":
                        case "MovementZPlus":
                        case "MovementZMinus":
                            HandleMovement(hitObject, tag);
                            break;
                        case "ZoomTarget":
                            break;
                        case "Inspect":
                            EnterInspectionMode(hitObject);
                            break;
                        default:
                            throw new System.Exception($"Tag {tag} was not recognized");
                    }
                }
            }
        }
    }

    private void EnterInspectionMode(GameObject inspectedObj)
    {
        _cameraMover.IsInAction = true;
        inspectedElement = inspectedObj.GetComponent<Sc_ObjectInspector>();
        inspectedElement.EnterInspectionMode();
    }

    private void ExitInspectionMode()
    {
        inspectedElement.ExitInspectionMode();
        inspectedElement = null;
        _cameraMover.IsInAction = false;
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
            case "RotateY":
                obj.transform.Rotate(0f, 180f, 0f);
                break;
        }

        Vector3 newPosition = obj.transform.position + offset;
        obj.transform.position = newPosition;
        obj.tag = newTag;
    }
}