using UnityEngine;
using System.Collections;

public class Lever : InteractableObject
{
    [SerializeField] private TwoLeversSystemDoor _door;
    private int _leverID;
    [SerializeField] private LeverID _id;
    [SerializeField] private float _AutoToggleTimer;

    public enum LeverID
    {
        Lever1,
        Lever2
    }

    private void Start()
    {
        _leverID = _id == LeverID.Lever1 ? IDManager.Lever1ID : IDManager.Lever2ID;
        _door.SetLeverID(_leverID);
    }
    public override void DoInteraction()
    {
        _door.ToggleLever(_leverID);

        if (InventoryManager.Instance.selectedItemID == IDManager.WaterBucketID)
        {
            Debug.Log("Ajouter seau sur levier");
            InventoryManager.Instance.RemoveItemFromID(IDManager.WaterBucketID);
            return;
        }
        else if(InventoryManager.Instance.selectedItemID == IDManager.EmptyBucketID)
        {
            Debug.Log("Dialogue : C'est pas assez lourd pour faire pression sur le levier");
        }
        StopAllCoroutines();
        StartCoroutine(RevertLeverTimer());
    }
    private IEnumerator RevertLeverTimer()
    {
        yield return new WaitForSeconds(_AutoToggleTimer);

        _door.ToggleLever(_leverID);
    }

}
