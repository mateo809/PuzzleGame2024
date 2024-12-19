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
        if (circuitBreaker.electricityIsCut && InventoryManager.Instance.selectedItemID == IDManager.ElectrickCableID)
        {
            circuitBreaker.powerIsRepare = true;
            _yellowFuse.SetActive(true);

            InventoryManager.Instance.RemoveItemFromID(IDManager.ElectrickCableID);

            Destroy(this);
        }
        else
        {
            HintManager.Instance.DisplayTextFromID(IDHints.HintCircuitBreaker);
        }

    }
}
