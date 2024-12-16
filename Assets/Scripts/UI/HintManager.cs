using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    public GameObject hintBox;
    public TextMeshProUGUI hintText;
    public List<string> hintString;
    [SerializeField] private float _textSpeed = 0.05f;
    [SerializeField] private PhoneManager _phoneManager;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ActivateHint(int index)
    {
        hintBox.SetActive(true);
        if (index == IDHints.HintCarTimer)
        {
            StopAllCoroutines();
            StartCoroutine(CloseHintCarTimer(index));
        }

        else
        {
            StopAllCoroutines();
            StartCoroutine(CloseHint(index));
        }
    }

    private IEnumerator CloseHint(int index)
    {
        string text = hintString[index];

        for (int i = 1; i < text.Length; i++)
        {
            hintText.text = text.Substring(0, i);
            yield return new WaitForSeconds(_textSpeed);
        }

        yield return new WaitForSeconds(3f);
        hintBox.SetActive(false);
    }

    private IEnumerator CloseHintCarTimer(int index)
    {
        float timeRemaining = 30 - _phoneManager._currentMinute;
        string textTimerCar = timeRemaining.ToString();
        string text = hintString[index];

        hintText.text = textTimerCar + " minutes";
        yield return new WaitForSeconds(2f);

        for (int i = 1; i < text.Length; i++)
        {
            hintText.text = text.Substring(0, i);
            yield return new WaitForSeconds(_textSpeed);
        }

        yield return new WaitForSeconds(3f);
        hintBox.SetActive(false);
    }
}

public static class IDHints
{
    static public int NoneHint = 0;
    static public int HintRoofKey = 1;
    static public int HintElectricalBox = 2;
    static public int HintMainEntrance = 3;
    static public int HintCarTimer = 4;
}