using UnityEngine;
using UnityEngine.Events;

public class Sc_Button : InteractableObject
{
    public string digicodeNumber = "";

    public UnityEvent DigicodeClicked;
    [SerializeField] private Sc_AudioSelection _audioSelection;
    public override void DoInteraction()
    {
        _audioSelection.PlaySound(Sc_IDSFXManager.digicodeButtonID);
        DigicodeClicked.Invoke();
    }
}
