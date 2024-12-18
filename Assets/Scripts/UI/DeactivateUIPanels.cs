using UnityEditor.SceneManagement;
using UnityEngine;

public class DeactivateUIPanels : MonoBehaviour
{
    [SerializeField] private GameObject _introGO;

    public void DeactivateIntro()
    {
        Destroy(_introGO);
        Destroy(GetComponent<Animator>());
        Destroy(this);
    }
}
