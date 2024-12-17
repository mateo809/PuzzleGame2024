using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private GameObject cameraControllerObject; // Référence au GameObject contenant Sc_CameraMovement
    [SerializeField] private Camera _inspectionCamera;
    [SerializeField, Tooltip("Speed multiplier for movement interactions.")]
    private float movementSpeed = 0.25f;

    private Sc_CameraMovement _cameraMovementScript;
    private Camera _currentCamera;

    private GameObject inspectedElement = null;

    private void Start()
    {
        _currentCamera = Camera.main;

        if (cameraControllerObject != null)
        {
            _cameraMovementScript = cameraControllerObject.GetComponent<Sc_CameraMovement>();
        }
        else
        {
            Debug.LogError("Camera Controller Object is not assigned!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
        if (Input.GetMouseButtonDown(1) && inspectedElement != null)
        {
            //ExitInspectionMode();
            inspectedElement.GetComponent<Sc_ObjectInspector>().ExitInspectionMode();
            inspectedElement = null;
        }
    }

    private void Interact()
    {
        if (_currentCamera == null) return;

        Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);
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

                if (_cameraMovementScript != null && !_cameraMovementScript.IsCameraFocused && tag == "ZoomTarget")
                {
                    _cameraMovementScript.Focus(hit.collider.gameObject.transform.localToWorldMatrix.GetPosition());
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

    private void EnterInspectionMode(GameObject hitObject)
    {
        //cameraControllerObject.gameObject.SetActive(false);
        ////_currentCamera.gameObject.SetActive(false);
        //_currentCamera = _inspectionCamera;
        //_currentCamera.gameObject.SetActive(true);
        inspectedElement = hitObject;
        inspectedElement.GetComponent<Sc_ObjectInspector>().EnterInspectionMode();
    }

    public void ExitInspectionMode()
    {
        if (_currentCamera != null && _currentCamera != Camera.main)
        {
            _currentCamera.gameObject.SetActive(false);
            if (cameraControllerObject != null)
            {
                cameraControllerObject.SetActive(true);
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
            case "RotateY":
                obj.transform.Rotate(0f, 180f, 0f);
                break;
        }

        Vector3 newPosition = obj.transform.position + offset;
        obj.transform.position = newPosition;
        obj.tag = newTag;
    }
}