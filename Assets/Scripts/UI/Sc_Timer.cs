using UnityEngine;

public class Sc_Timer : MonoBehaviour
{
    [SerializeField] private PhoneManager _timer;
    [SerializeField] private float _endHour = 9;
    [SerializeField] private float _endMinutes = 30;

    private void Update()
    {
        TimerIsOver();
    }

    private void TimerIsOver()
    {
        if(_timer._currentHour == _endHour && _timer._currentMinute == _endMinutes)
        {
            Debug.Log("timer is Over");
        }
    }
}