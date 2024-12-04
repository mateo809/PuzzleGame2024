using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID; 

    private void Start()
    {
        KeyDoorManager.Instance.RegisterKey(keyID);
    }
}
