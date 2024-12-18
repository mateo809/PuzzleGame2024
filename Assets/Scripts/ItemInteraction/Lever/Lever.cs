using UnityEngine;
using System.Collections;

public class Lever : InteractableObject
{
    [SerializeField] private TwoLeversSystemDoor _door;
    private int _leverID;
    private bool _hasBucket = false;
    [SerializeField] private LeverID _id;
    [SerializeField] private float _AutoToggleTimer;

    [SerializeField] private GameObject _leverReference; 
    private GameObject _childToActivate;

    public enum LeverID
    {
        Lever1,
        Lever2
    }

    private void Start()
    {
        _leverID = _id == LeverID.Lever1 ? IDManager.Lever1ID : IDManager.Lever2ID;
        _door.SetLeverID(_leverID);
        if (_leverReference != null)
        {
            Transform firstChildTransform = _leverReference.transform.GetChild(0); 
            if (firstChildTransform != null)
            {
                _childToActivate = firstChildTransform.gameObject;
                _childToActivate.SetActive(false);  
            }
        }
    }

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.WaterBucketID)
        {

            Debug.Log("Ajouter seau sur levier");
            InventoryManager.Instance.RemoveItemFromID(IDManager.WaterBucketID);

            _hasBucket = true;
            _door.ToggleLever(_leverID);
            if (_childToActivate != null)
            {
                _childToActivate.SetActive(true); 
            }
            return;
        }
        else if (InventoryManager.Instance.selectedItemID == IDManager.EmptyBucketID)
        {
            Debug.Log("Dialogue : C'est pas assez lourd pour faire pression sur le levier");
            return;
        }
        _hasBucket = false; 
        _door.ToggleLever(_leverID);
        if (!_hasBucket)
        {
            StopAllCoroutines();
            StartCoroutine(RevertLeverTimer());
        }
    }

    private IEnumerator RevertLeverTimer()
    {
        yield return new WaitForSeconds(_AutoToggleTimer);
        if (!_hasBucket)
        {
            _door.ToggleLever(_leverID);
        }
    }
}
