using UnityEngine;

public class CableBox : InteractableObject
{
    public CircuitBreaker circuitBreaker;

    [SerializeField] private GameObject _yellowFuse;


    private void Start()
    {
        _yellowFuse.SetActive(false);
    }

    public override void DoInteraction()
    {
        if (circuitBreaker.electricityIsCut)
        {
            if (InventoryManager.Instance.selectedItemID == IDManager.ElectrickCableID) 
            {
                circuitBreaker.powerIsRepare = true;
                _yellowFuse.SetActive(true);
                InventoryManager.Instance.RemoveItemFromID(IDManager.ElectrickCableID);
                Debug.Log("The cable is repare !");
                Destroy(this);

            }
            else
            {
                Debug.Log("Need electrick cable for energy");
            }
        }
        else
        {
            Debug.Log("I need to cut electricity");
        }

    }
}
