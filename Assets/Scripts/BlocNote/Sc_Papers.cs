using UnityEngine;
using UnityEngine.UI;

public class Sc_Papers : InteractableObject
{
    [SerializeField] private Image _paperCarnet;
    [SerializeField] private Sc_AudioSelection _audioSelection;

    public override void DoInteraction()
    {
        _audioSelection.PlaySound(Sc_IDSFXManager.pickUpPaperID);
        _paperCarnet.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
