using UnityEngine;

public class CircuitBreaker : InteractableObject
{
    public bool electricityIsCut = false;
    public bool powerIsRepare = false;

    public ElectricityManager electricityManager;
    [SerializeField] private GameObject _electrickParticles;
    [SerializeField] private GameObject _duck;

    [SerializeField] private Animator _animator;
    [SerializeField] private Sc_AudioSelection _audioSelection;

    public override void DoInteraction()
    {
        electricityIsCut = !electricityIsCut;
        ToggleButtonCircuitBreaker();
    }

    private void ToggleButtonCircuitBreaker()
    {
        if (electricityIsCut)
        {
            // Button up
            Debug.Log("OFF");
            _audioSelection.PlaySound(Sc_IDSFXManager.switchLightOffID);
            _animator.SetBool("Activate", false);
            _electrickParticles.SetActive(false);
        }
        else
        {
            // Button down
            Debug.Log("ON");
            _audioSelection.PlaySound(Sc_IDSFXManager.switchLightOnID);
            _animator.SetBool("Activate", true);
            _electrickParticles.SetActive(true);
            if (powerIsRepare)
            {
                Debug.Log("The electricity is back!");
                electricityManager.OnElectricityRestored();
                Destroy(_electrickParticles);
            }
        }
    }
    public void ActiveAllEnergy()
    {
        Debug.Log("Duck is out");
        _duck.SetActive(true);
        Destroy(this); 
    }
}
