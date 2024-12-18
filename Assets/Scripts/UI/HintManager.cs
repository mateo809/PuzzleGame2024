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

    public void Start()
    {
        _introBackground.SetActive(true);
        _startButton.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(PlayHint(IDHints.Intro));
    }

    public void ActivateHint(int index)
    {
        hintBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayHint(index));
    }

    private IEnumerator PlayHint(int index)
    {
        string text = hintString[index];

        if (index == IDHints.HintCarTimer)
        {
            float timeRemaining = _endMinutes - _phoneManager._currentMinute;
            string textTimerCar = timeRemaining.ToString();
            text = hintString[index] + textTimerCar + " minutes left. ";
        }

        if (index == IDHints.Intro && !introIsOver)
        {
            for (int i = 1; i < text.Length; i++)
            {
                introText.text = text.Substring(0, i);
                yield return new WaitForSeconds(_textSpeed);
            }
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

    public void ButtonStart()
    {
        _introBackground.SetActive(false);
        _phoneManager._currentMinute = _phoneManager._startMinute;
        _phoneManager._currentSecond = _phoneManager._startSecond;
        Time.timeScale = 1;
    }
}

public static class IDHints
{
    static public int NoneHint = 0;
    static public int HintRoofKey = 1;
    static public int HintElectricalBox = 2;
    static public int HintMainEntrance = 3;
    static public int HintCarTimer = 4;
    static public int Intro = 10;
}