using UnityEngine;
using UnityEngine.Events;

public class Sc_Button : MonoBehaviour
{
    public string digicodeNumber = "";

    public UnityEvent DigicodeClicked;

    private void OnMouseDown()
    {        
        DigicodeClicked.Invoke();
    }
}
