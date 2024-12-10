using UnityEngine;

public class Lever : InteractableObject
{
    [SerializeField] private TwoLeversSystemDoor _door;
    [SerializeField] private int _leverID;

    private void Start()
    {
        _door.SetLeverID(_leverID);
    }
    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _interactionID) //Check water bucket
        {
            Debug.Log("weighted");
            InventoryManager.Instance.RemoveCurrItem(_interactionID);
            _door.ToggleLever(_leverID, true);
            Destroy(this);
        }
        else if(InventoryManager.Instance.selectedItemID == -1)
        {
            Debug.Log("NOweighted");
            _door.ToggleLever(_leverID, false);
        }
    }

}
