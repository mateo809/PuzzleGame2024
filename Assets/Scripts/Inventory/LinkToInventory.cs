using UnityEngine;
using UnityEngine.UI;

public class LinkToInventory : MonoBehaviour
{
    private UIinteractImage _UIinteractImage;
    public itemData data;

    public void Init(itemData newData)
    {
        data = newData;
        GetComponentInChildren<Button>().image.sprite = data.sprite;
        _UIinteractImage = GetComponentInParent<UIinteractImage>();
    }

    public void SelectItem()
    {
        InventoryManager.Instance.SetSelectedItem(data.itemID);

    }
}
