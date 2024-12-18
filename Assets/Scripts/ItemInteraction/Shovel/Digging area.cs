using System.Collections;
using UnityEngine;

public class DiggingArea : InteractableObject
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private GameObject _paper;
    [SerializeField] private Sc_AudioSelection _audioSelection;

    public override void DoInteraction()
    {
        if (InventoryManager.Instance.selectedItemID == IDManager.ShovelID) 
        {
            _audioSelection.PlaySound(Sc_IDSFXManager.stepGrassID);
            StartCoroutine(AnimShovel());
        }
    }

    private IEnumerator AnimShovel()
    {
        _Animator.SetBool("Dig", true);
        yield return new WaitForSeconds(2f);
        _Animator.SetBool("Dig", false);
        InventoryManager.Instance.RemoveItemFromID(IDManager.ShovelID);
        _paper.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}