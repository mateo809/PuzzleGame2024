using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    public GameObject hintBox;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI introText;

    public bool introIsOver = false;
    public int introID;
    public List<string> hintString;

    [SerializeField] private float _textSpeed = 0.05f;
    [SerializeField] private float _endMinutes = 30;
    [SerializeField] private PhoneManager _phoneManager;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void DisplayTextFromID(int p_index)
    {
        hintBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(DisplayText(hintString[p_index], true));
    }

    public void DisplayTextFromString(string textToDiplay, bool autoDeactivate)
    {
        hintBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(DisplayText(textToDiplay, autoDeactivate));
    }

    private IEnumerator DisplayText(string textToDiplay, bool autoDeactivate)
    {
        if (textToDiplay == hintString[IDHints.HintCarTimer])
        {
            float timeRemaining = _endMinutes - _phoneManager._currentMinute;
            string textTimerCar = timeRemaining.ToString();
            textToDiplay = hintString[IDHints.HintCarTimer] + textTimerCar + " minutes left. ";
        }

        for (int i = 1; i <= textToDiplay.Length; i++)
        {
            hintText.text = textToDiplay.Substring(0, i);
            yield return new WaitForSeconds(_textSpeed);
        }

        if (!autoDeactivate)
            yield return null;
        else
        {
            yield return new WaitForSeconds(3f);
            hintBox.SetActive(false);
        }
    }

    public void DeactivateHintBox()
    {
        hintBox.SetActive(false);
    }


}

public static class IDHints
{
    static public int NoneHint = 0;
    static public int HintRoofKey = 1;
    static public int HintCircuitBreaker = 2;
    static public int HintMainEntrance = 3;
    static public int HintCarTimer = 4;
    static public int InventoryFull = 5;
}