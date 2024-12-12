using UnityEngine;

public class ExitRoom : InteractableObject
{
    [SerializeField] private GameObject _mainMap;
    [SerializeField] private GameObject _currentMap;

    public override void DoInteraction()
    {
        _mainMap.SetActive(true);
        _currentMap.SetActive(false);
    }
}
