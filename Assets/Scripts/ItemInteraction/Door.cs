using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Door : InteractableObject
{

    public bool IsFinalDoor = false;
    [SerializeField] private TextMeshProUGUI _WinText;

    [SerializeField]
    private UIinteractImage uIinteractImage;



    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == RequiredItemID)
        {
            Debug.Log("Open");
            InventoryManager.Instance.RemoveCurrItem(RequiredItemID);
            if (IsFinalDoor)
            {
                _WinText.gameObject.SetActive(true);
                Debug.Log("Final Door Unlocked!");
            }
            Destroy(this);
        }
        else
        {
            Debug.Log("Pas la bonne chef");
        }
    }
}
