using UnityEngine;
using UnityEngine.UI;

public class Sc_Papers : InteractableObject
{
    [SerializeField] private Image _paperCarnet;

    public override void DoInteraction()
    {
        _paperCarnet.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
