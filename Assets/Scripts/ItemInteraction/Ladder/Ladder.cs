using UnityEngine;

public class Ladder : InteractableObject
{ 
    [SerializeField] private MeshRenderer _ladderPlacedMesh;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.LadderID) //Check ladder
        {            
            _ladderPlacedMesh.enabled = true;

            InventoryManager.Instance.RemoveItemFromID(IDManager.LadderID);
        }
    }
}
