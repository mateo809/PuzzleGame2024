using System.Collections;
using TMPro;
using UnityEngine;

public class RoofKey : InteractableObject
{
    [SerializeField] private MeshRenderer _ladderPlaceMesh;
    [SerializeField] private itemData _itemData;
    public override void DoInteraction()
    {
        if (_ladderPlaceMesh.enabled == false)
        {
            HintManager.Instance.DisplayTextFromID(IDHints.HintRoofKey);
        }
        else if(InventoryManager.Instance.TryAddToInventory(_itemData))
        {
            Destroy(gameObject);
        }
    }
}
