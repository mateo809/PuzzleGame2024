using UnityEngine;

public class ItemWaypoint : MonoBehaviour
{
    [Range(1,3)] public int itemSize;    //1 = Small, 2 = Medium, 3 = Large
    public bool canBePreviewed;
    public Collider _colliderToDisable;
}
