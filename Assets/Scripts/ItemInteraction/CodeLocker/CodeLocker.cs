using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CodeLocker : MonoBehaviour
{
    [SerializeField] private List<Transform> _wheelList = new List<Transform>(); 
    [SerializeField] private List<int> _correctCode = new List<int>(); 
    private List<int> _currentCode = new List<int>(); 

    private void Start()
    {
        for (int i = 0; i < _wheelList.Count; i++)
        {
            _currentCode.Add(0);
        }
    }

    private void DetectClickedWheel()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction.normalized * 1000, Color.red, 1);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (!hit.collider.gameObject.CompareTag("Wheel"))
                return;
            int wheelIndex = _wheelList.FindIndex(wheel => wheel == hit.transform); // find index hit 

            if (wheelIndex != -1)
            {
                RotateWheel(hit.transform, wheelIndex);
            }
        }
    }

    private void RotateWheel(Transform wheel, int index)
    {
        wheel.Rotate(wheel.forward, -36);
        _currentCode[index]++;
        if (_currentCode[index] == 10) _currentCode[index] = 0;
        CheckCode();
    }

    private void CheckCode()
    {
        if (_currentCode.SequenceEqual(_correctCode)) //check if the two List are identical
        {
            Debug.Log("Open");
            Destroy(gameObject);
        }
    }


    private void OnMouseDown()
    {
        DetectClickedWheel();
    }
}
