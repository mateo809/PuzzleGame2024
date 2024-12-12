using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    public List<PlaneData> planeDataList;
    public CircuitBreaker circuitBreaker;

    public void OnElectricityRestored()
    {
        foreach (var planeData in planeDataList)
        {
            planeData.DeactivateShadows();
        }

        if (circuitBreaker != null)
        {
            circuitBreaker.ActiveAllEnergy();  
        }
    }
}
