using UnityEngine;

public class Sc_ExitMap : InteractableObject
{
    [SerializeField] private GameObject _mapToActivate;
    [SerializeField] private GameObject _mapToDesactivate;
    [SerializeField] private Transform _cameraPivotTrans;
    public bool isUnlocked = false;
      
    public override void DoInteraction()
    {
        if (isUnlocked == true)
        {
            _mapToDesactivate.SetActive(false);
            _mapToActivate.SetActive(true);            
            _cameraPivotTrans.eulerAngles = new Vector3(_cameraPivotTrans.eulerAngles.x, 90, _cameraPivotTrans.eulerAngles.z);            
        }
    }
}
