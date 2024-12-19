using System.Collections.Generic;
using UnityEngine;

public class Sc_BackgorundPanel : MonoBehaviour
{
    public List<GameObject> _panelsToDeactivate;
    [SerializeField] private List<bool> _panelsToReactivate;

    public void OnEnable()
    {
        for(int i = 0; i < _panelsToDeactivate.Count; i++)
        {
            if( _panelsToDeactivate[i] != null)
            {
                if (_panelsToDeactivate[i].active)
                {
                    _panelsToReactivate[i] = true;
                } 
                else 
                {
                    _panelsToReactivate[i] = false;
                }
                _panelsToDeactivate[i].active = false;
            }
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < _panelsToReactivate.Count; i++)
        {
            if (_panelsToReactivate[i])
            {
                _panelsToDeactivate[i].active = true;
            }
            else
            {
                _panelsToDeactivate[i].active = false;
            }
            _panelsToReactivate[i] = false;
        }
    }

}
