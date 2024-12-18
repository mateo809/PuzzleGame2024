using System.Collections.Generic;
using UnityEngine;

public class Sc_Intro : MonoBehaviour
{
    public List<string> introString;

    [SerializeField] private PhoneManager _phoneManager;
    [SerializeField] private GameObject _introBackground;

    public void ButtonNext()
    {

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
