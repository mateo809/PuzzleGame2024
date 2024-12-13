using UnityEngine;
using UnityEngine.Events;

public class Sc_Button : InteractableObject
{
    public string digicodeNumber = "";

    public UnityEvent DigicodeClicked;

    public override void DoInteraction()
    {        
        DigicodeClicked.Invoke();
    }
}
