using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected int _interactionID = IDManager.NoneInteractableObj;

    public virtual void DoInteraction()
    {
        Debug.Log("interactable obj");

    }

}
