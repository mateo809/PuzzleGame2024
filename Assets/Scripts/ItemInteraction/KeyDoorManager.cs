using System.Collections.Generic;
using UnityEngine;
public class KeyDoorManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public static KeyDoorManager Instance;
    private Dictionary<string, bool> _keyStates = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

 

    public void RegisterKey(string keyID) //initialization clé
    {
        if (!_keyStates.ContainsKey(keyID))
        {
            _keyStates.Add(keyID, false);
        }
    }

    public void PickUpKey(string keyID) // ramasser la clé
    {
        if (_keyStates.ContainsKey(keyID))
        {
            _keyStates[keyID] = true; 
            Debug.Log($"Clé {keyID} ramassée !");
        }
    }
    public bool HasKey(string keyID) // check si la cle existe
    {
        return _keyStates.ContainsKey(keyID) && _keyStates[keyID];
    }
}
