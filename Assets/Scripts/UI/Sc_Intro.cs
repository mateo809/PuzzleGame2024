using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_Intro : MonoBehaviour
{
    public List<string> _introTexts = new List<string>();

    [SerializeField] private HintManager _hintManager;
    private int _currTextIndex = 0;


    public void Start()
    {
        DisplayNextIntroText();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            DisplayNextIntroText();
        }
    }

    private void DisplayNextIntroText()
    {
        _hintManager.DeactivateHintBox();

        if (_currTextIndex >= _introTexts.Count)
        {
            StartGame();
            return;
        }

        string text = _introTexts[_currTextIndex];

        if (text.Substring(0, 4) == "Play")
            PlayNextIntroSound(text);
        else
            _hintManager.DisplayTextFromString(text, false);

        _currTextIndex++;
    }

    private void PlayNextIntroSound(string audioInstruction)
    {
        Debug.Log("CALL AUDIO MANAGER/PLAYER IF INSERTED");

        string audio = audioInstruction.Substring(4);

        switch (audio)
        {
            case "DoorLock":
                Debug.Log("Door Locked Sound");
                // Sc_AudioSelection.PlaySound(Sc_IDSFXManager.DoorLockID);
                break;
            case "KeyDrop":
                Debug.Log("Key Drop Sound");
                // Sc_AudioSelection.PlaySound(Sc_IDSFXManager.KeyDropID);
                break;
            case "Meow":
                Debug.Log("Meow Sound");
                // Sc_AudioSelection.PlaySound(Sc_IDSFXManager.CatMeowID);
                break;
            case "JiggleKeys":
                Debug.Log("Jiggle Keys Sound");
                // Sc_AudioSelection.PlaySound(Sc_IDSFXManager.JiggleKeysID);
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        _hintManager.DeactivateHintBox();
        gameObject.SetActive(false);
    }
}
