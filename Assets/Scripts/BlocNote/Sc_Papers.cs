using UnityEngine;
using UnityEngine.UI;

public class Sc_Papers : MonoBehaviour
{
    [SerializeField] private Image _paperCarnet;

    private void OnMouseDown()
    {
        _paperCarnet.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
