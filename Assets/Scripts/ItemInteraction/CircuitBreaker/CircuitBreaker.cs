using UnityEngine;

public class CircuitBreaker : InteractableObject
{
    public bool electricityIsCut = false;
    public bool powerIsRepare = false;

    public ElectricityManager electricityManager;

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
        }
        else
        {
            // Button down
            Debug.Log("ON");
            _animator.SetBool("Activate", true);
            if (powerIsRepare)
            {
                Debug.Log("The electricity is back!");
                electricityManager.OnElectricityRestored();  
            }
        }
    }
    public void ActiveAllEnergy()
    {
        Debug.Log("Duck is out");
        Destroy(this); 
    }
}
