using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BackgorundPanel : MonoBehaviour
{
    public List<GameObject> _panelsToDeactivate = new List<GameObject>() { };
    [SerializeField] private List<bool> _panelsToReactivate;

    public void Awake()
    {
        for(int i = 0; i < _panelsToDeactivate.Count; i++)
        {
            _panelsToReactivate.Add(false);
        }
    }

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

}
