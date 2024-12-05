using System.Collections.Generic;
using UnityEngine;
public class KeyDoorManager : MonoBehaviour
{
    public static KeyDoorManager Instance;
    private Dictionary<string, bool> _keyStates = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectItem();
        }
    }

    private void DetectItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Item"))
            {
                Key keyItem = hit.collider.gameObject.GetComponent<Key>();
                if (keyItem != null)
                {
                    PickUpKey(keyItem.keyID);
                    Destroy(hit.collider.gameObject);
                }
            }
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
