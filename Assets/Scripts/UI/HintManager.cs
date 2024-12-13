using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    public GameObject hintBox;
    public TextMeshProUGUI hintText;
    //public List<int> hintInt;
    public List<string> hintString;
    //public Dictionary<int, string> hintString;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ActivateHint(int index)
    {
        hintBox.SetActive(true);
        hintText.text = hintString[index];
        StartCoroutine(CloseDialogue());
    }

    /*public void ChangeHint(int index)
    {
        hintText.text = hintString[index];
    }*/

    private IEnumerator CloseDialogue()
    {
        yield return new WaitForSeconds(3f);
        hintBox.SetActive(false);
    }
}

public static class IDHints
{
    static public int NoneHint = 0;
    static public int HintRoofKey = 1;
    static public int HintElectricalBox = 2;
}