using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    public GameObject hintBox;
    public TextMeshProUGUI hintText;
    public List<string> hintString;
    [SerializeField] private float _textSpeed = 0.05f;

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
        StopAllCoroutines();
        StartCoroutine(CloseDialogue(index));
    }

    private IEnumerator CloseDialogue(int index)
    {
        string text = hintString[index];

        for (int i = 1; i < text.Length; i++)
        {
            hintText.text = text.Substring(0, i);
            yield return new WaitForSeconds(_textSpeed);
        }

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