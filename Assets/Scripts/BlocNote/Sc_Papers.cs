using UnityEngine;
using UnityEngine.UI;

public class Sc_Papers : InteractableObject
{
    [SerializeField] private Image _paperCarnet;
    [SerializeField] private Sc_AudioSelection _audioSelection;
    [SerializeField] private bool _isFirstPaper;

    public override void DoInteraction()
    {
        if (_isFirstPaper) 
            HintManager.Instance.DisplayTextFromID(IDHints.FirstPaperHint);
        _audioSelection.PlaySound(Sc_IDSFXManager.pickUpPaperID);
        _paperCarnet.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
