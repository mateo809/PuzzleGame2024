using System.Collections;
using TMPro;
using UnityEngine;

public class RoofPaper : MonoBehaviour
{
    [SerializeField] private MeshRenderer _ladderPlaceMesh;
    [SerializeField] private TextMeshProUGUI _ladderText;
    private void OnMouseDown()
    {
        if (_ladderPlaceMesh.enabled == false)
        {
            StartCoroutine(StringLadder());
        }      
        else
        {
            Debug.Log("Get it");
        }
    }

    private IEnumerator StringLadder()
    {
        _ladderText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _ladderText.gameObject.SetActive(false);
    }
}
