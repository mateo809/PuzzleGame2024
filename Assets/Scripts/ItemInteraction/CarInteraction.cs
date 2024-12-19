using UnityEngine;

public class CarInteraction : InteractableObject
{
    [SerializeField] Sc_AudioSelection _audioSelect;
    public override void DoInteraction()
    {
        _audioSelect.PlaySound(Sc_IDSFXManager.lockedCarDoorID);
        HintManager.Instance.DisplayTextFromID(IDHints.HintCarTimer);
    }
}
