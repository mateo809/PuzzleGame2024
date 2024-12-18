using System.Collections.Generic;
using UnityEngine;

public class Sc_Intro : MonoBehaviour
{
    public List<string> introString;

    [SerializeField] private PhoneManager _phoneManager;
    [SerializeField] private HintManager _hintManager;
    [SerializeField] private GameObject _introBackground;

    public void Start()
    {
        _introBackground.SetActive(true);
        _startButton.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(PlayHint(hintString[IDHints.Intro]));
    }

    public void ButtonNext()
    {
        switch (_hintManager.introID) 
        {
            case 10:

            case 11:

            case 12:

            case 13:

                ButtonStart()
            break;
        }
        introString[introString++];
        _phoneManager._currentMinute = _phoneManager._startMinute;
        _phoneManager._currentSecond = _phoneManager._startSecond;
        Time.timeScale = 1;
    }

    public void ButtonStart()
    {
        _introBackground.SetActive(false);
        _phoneManager._currentMinute = _phoneManager._startMinute;
        _phoneManager._currentSecond = _phoneManager._startSecond;
        Time.timeScale = 1;
    }
}
