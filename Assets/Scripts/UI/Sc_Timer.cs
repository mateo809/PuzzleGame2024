using UnityEngine;

public class Sc_Timer : MonoBehaviour
{
    [SerializeField] private PhoneManager _timer;

    private void Update()
    {
        TimerIsOver();
    }

    private void TimerIsOver()
    {
        if(_timer._currentHour == 9 && _timer._currentMinute == 30)
        {
            Debug.Log("timer is Over");
        }
    }
}