using UnityEngine;

public class Ladder : InteractableObject
{ 
    [SerializeField] private MeshRenderer _ladderPlacedMesh;
    [SerializeField] private GameObject _keyShed;
    [SerializeField] private Sc_AudioSelection _audioSelection;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.LadderID) //Check ladder
        {
            _keyShed.layer = LayerMask.NameToLayer("InteractableObject");
            _audioSelection.PlaySound(Sc_IDSFXManager.ladderFittingID);
            _ladderPlacedMesh.enabled = true;

            InventoryManager.Instance.RemoveItemFromID(IDManager.LadderID);
        }
    }
}
