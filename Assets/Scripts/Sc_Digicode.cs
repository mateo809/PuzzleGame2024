using System.Collections;
using TMPro;
using UnityEngine;

public class Sc_Digicode : MonoBehaviour
{
    public string passwordDigicode = "6428";
    private string _userInput = "";
    private bool _canUserInputing = true;
    [SerializeField] private GameObject _keyGameObject;

    public TextMeshPro userInputText;

    private void Start()
    {
        _userInput = "";
        userInputText.text = _userInput;
    }

    public void ButtonClicked (string number)
    {
        if (_userInput.Length < 4 && _canUserInputing)
        {
            _userInput += number;
            userInputText.text = _userInput;
        }
    }   

    private IEnumerator RefreshDigicode()
    {
        yield return new WaitForSeconds(1.5f);

        userInputText.color = Color.black;
        _userInput = "";
        userInputText.fontSize = 4;
        userInputText.text = _userInput;
        _canUserInputing = true;
    }

    public void EnterButton()
    {
        if (_canUserInputing)
        {
            if (_userInput.Length >= 4 && _userInput == passwordDigicode)
            { 
                userInputText.color = Color.green;
                userInputText.fontSize = 2;
                userInputText.text = "Correct code";
                Destroy(gameObject);
                _keyGameObject.gameObject.SetActive(true);
                _canUserInputing = false;
                StartCoroutine(RefreshDigicode());
            }
            else
            {
                userInputText.color = Color.red;
                userInputText.fontSize = 2;
                userInputText.text = "Incorrect code";
                _canUserInputing = false;
                StartCoroutine(RefreshDigicode());
            }
        }
    }

    public void ClearButton()
    {
        if (_canUserInputing)
        {
            _userInput = "";
            userInputText.text = _userInput;
        }
    }
}
    