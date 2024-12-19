using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_MenusManager : MonoBehaviour
{
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private Toggle _fullScreenToggle;
    //Main Menu

    private void Start()
    {
        Screen.fullScreen = _fullScreenToggle.isOn;
        Time.timeScale = 1.0f;
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaveButton()
    {
        Application.Quit();
    }

    //Settings Menu
    public void ToggleFullscreen()
    {
        Screen.fullScreen = _fullScreenToggle.isOn;
    }

    //Pause Menu
    public void ButtonMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateAudioPanel()
    {
        _audioPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DesactivateAudioPanel()
    {
        _audioPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
