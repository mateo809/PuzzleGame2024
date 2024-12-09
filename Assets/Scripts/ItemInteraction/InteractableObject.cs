using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected int _interactionID = -1;

    public virtual void DoInteraction()
    {
        Debug.Log("interactable obj");

    }

}
