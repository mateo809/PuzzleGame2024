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
    [SerializeField] private GameObject _introBackground;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private PhoneManager _phoneManager;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    

    public void ActivateHint(string p_index)
    {
        hintBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(DisplayText(p_index));
    }

    public IEnumerator DisplayText(string p_index)
    {
        string text = p_index;

        if (p_index == hintString[IDHints.HintCarTimer])
        {
            float timeRemaining = _endMinutes - _phoneManager._currentMinute;
            string textTimerCar = timeRemaining.ToString();
            text = hintString[IDHints.HintCarTimer] + textTimerCar + " minutes left. ";
        }

        if (p_index == hintString[IDHints.Intro] && !introIsOver)
        {
            for (int i = 1; i < text.Length; i++)
            {
                introText.text = text.Substring(0, i);
                yield return new WaitForSeconds(_textSpeed);
            }
            introID = IDHints.Intro;
            introIsOver = true;
            Time.timeScale = 0;
            _startButton.SetActive(true);
        }

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
    static public int HintCircuitBreaker = 2;
    static public int HintMainEntrance = 3;
    static public int HintCarTimer = 4;
    static public int InventoryFull = 5;
}