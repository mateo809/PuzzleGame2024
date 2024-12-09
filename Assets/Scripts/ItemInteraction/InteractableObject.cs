using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected int RequiredItemID = -1;

    public virtual void DoInteraction()
    {
        Debug.Log("interactable obj");
    }

}
