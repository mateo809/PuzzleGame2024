using UnityEngine;

public class Lever : InteractableObject
{
    [SerializeField] private TwoLeversSystemDoor _door;
    public override void DoInteraction()
    {
        _door.ToggleLever(_interactionID);
    }

}
