using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_MenusManager : MonoBehaviour
{
    public Sc_AudioSelection Sc_AudioSelection;

    //Main Menu
    public void ButtonPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LeaveButton()
    {
        Application.Quit();
        Sc_AudioSelection.PlaySound(Sc_IDSFXManager.ClosingDoorID);
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
