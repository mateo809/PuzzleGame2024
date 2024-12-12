using UnityEngine;

public class Lever : InteractableObject
{
    [SerializeField] private TwoLeversSystemDoor _door;
    [SerializeField] private int _leverID;
    [SerializeField] private LeverID _id;

    public enum LeverID
    {
        Lever1,
        Lever2
    }

    private void Start()
    {
        _leverID = _id == LeverID.Lever1 ? IDManager.Lever1ID : IDManager.Paper2ID;
        _door.SetLeverID(_leverID);
    }
    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.WaterBucketID) //Check water bucket
        {
            Debug.Log("weighted");
            InventoryManager.Instance.RemoveItemFromID(IDManager.WaterBucketID);
            _door.ToggleLever(_leverID, true);
            Destroy(this);
        }
        else if (InventoryManager.Instance.selectedItemID == IDManager.EmptyBucketID)
        {
            Debug.Log("The bucket is empty");
        }
        else if(InventoryManager.Instance.selectedItemID == IDManager.NoneItem)
        {
            Debug.Log("NOweighted");
            _door.ToggleLever(_leverID, false);
        }
    }

}
