using UnityEngine;

public class Ladder : InteractableObject
{
    [SerializeField] private int _ladderID;   
    [SerializeField] private MeshRenderer _ladderPlacedMesh;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _ladderID) //Check ladder
        {            
            _ladderPlacedMesh.enabled = true;

            InventoryManager.Instance.RemoveItemFromID(_ladderID);
        }
    }
}
