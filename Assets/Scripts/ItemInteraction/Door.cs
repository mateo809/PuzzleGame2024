using TMPro;
using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private TextMeshProUGUI _WinText;

    [SerializeField] private UIinteractImage _UIInteractImage;

    [SerializeField] private DoorID _id;

    [SerializeField] private Sc_ExitMap _exitMap;

    [SerializeField] private Sc_AudioSelection _audioSelection;

    [SerializeField] private GameObject _endPanel;
    public enum DoorID
    {
        HouseDoor,
        ShedDoor
    }
    private void Start()
    {
        _interactionID = _id == DoorID.HouseDoor ? IDManager.FinalDoorKeyID : IDManager.ShedDoorKeyID;
    }

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == _interactionID)
        {
            _audioSelection.PlaySound(Sc_IDSFXManager.openingDoorID);
            InventoryManager.Instance.RemoveItemFromID(_interactionID);

            if (_id == DoorID.HouseDoor) 
            {
                Time.timeScale = 0f;
                _endPanel.SetActive(true);

            }
            else if(_id == DoorID.ShedDoor)
            {
                _exitMap.isUnlocked = true;
            }
            Destroy(this);
        }
        else
        {
            _audioSelection.PlaySound(Sc_IDSFXManager.lockedDoorID);
            if (_id == DoorID.HouseDoor) HintManager.Instance.DisplayTextFromID(IDHints.HintMainEntrance);

        }
    }
}
