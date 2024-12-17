using UnityEngine;

public class CircuitBreaker : InteractableObject
{
    public bool electricityIsCut = false;
    public bool powerIsRepare = false;

    public ElectricityManager electricityManager;
    [SerializeField] private GameObject _electrickParticles;

    [SerializeField] private Animator _animator;

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
            _animator.SetBool("Activate", false);
            _electrickParticles.SetActive(false);
        }
        else
        {
            // Button down
            Debug.Log("ON");
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
        Destroy(this); 
    }
}
