using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;
    Camera cam;

    // Create a list to store interactable objects
    private List<IInteractable> inventory = new List<IInteractable>();


    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 1000000))
            {
                CheckInteracte();
                print("CLIKCKKK");
            }
        }
    }

    private void CheckInteracte()
    {
        // Start a new interaction
    
        Collider[] colliders = Physics.OverlapSphere(InteractorSource.position, InteractRange, interactableLayer);
        foreach (Collider collider in colliders)
        {
          
            if (collider.TryGetComponent<IInteractable>(out var interactObj))
            {
               
                // Check if the interactable object is within the specified range
                float distance = Vector3.Distance(InteractorSource.position, collider.transform.position);
                if (distance <= InteractRange &&  !inventory.Contains(interactObj))
                {
                    
                    interactObj.Interact();  
                    inventory.Add(interactObj);
                }
            }
        }
    }


}
