using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_MenusManager : MonoBehaviour
{
    //Main Menu
    public void ButtonPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LeaveButton()
    {
        Application.Quit();        
    }

    //Settings Menu
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    //Pause Menu
    public void ButtonMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
