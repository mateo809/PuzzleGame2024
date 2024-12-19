using System.Collections.Generic;
using UnityEngine;

public class Sc_Intro : MonoBehaviour
{
    public List<string> _introTexts = new List<string>();
    [SerializeField] private GameObject _map;
    [SerializeField] private Animator _animator;

    [SerializeField] private HintManager _hintManager;
    private int _currTextIndex = 0;

    [SerializeField] private Sc_AudioSelection _selection;
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
               _selection.PlaySound(Sc_IDSFXManager.lockingDoorID);
                break;
            case "KeyDrop":
                Debug.Log("Key Drop Sound");
               _selection.PlaySound(Sc_IDSFXManager.droppingKeyID);
                break;
            case "Meow":
                Debug.Log("Meow Sound");
               _selection.PlaySound(Sc_IDSFXManager.catMeowID);
                break;
            case "JiggleKeys":
                Debug.Log("Jiggle Keys Sound");
               _selection.PlaySound(Sc_IDSFXManager.keyJingleID);
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        _animator.SetBool("Finish",true);
        _hintManager.DeactivateHintBox();
        _map.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
