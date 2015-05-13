using UnityEngine;
using System.Collections;

public class GUIGHPauseMenu : MonoBehaviour
{
    GameObject waterMark;

    void Awake()
    {
        GameGUI.GHPauseMenu = transform.parent.FindChild("GH Pause Menu").gameObject;
        GameGUI.GHPauseMenu.SetActive(false);

        waterMark = transform.parent.FindChild("WaterMark").gameObject;
    }


    public void ContinueButton()
    {
        GameFlow.pause = false;
        Deactivate();
    }


    public void CreditsButton()
    {
        GameGUI.GHMainMenu.SetActive(true);
        GameGUI.GHMainMenu.transform.FindChild("Credits Menu").gameObject.SetActive(true);
    }


    public void OptionsButton()
    {
        GameGUI.GHMainMenu.SetActive(true);
        GameGUI.GHMainMenu.transform.FindChild("Options Menu").gameObject.SetActive(true);
    }


    public void MainMenuButton()
    {
        GameFlow.gameState = GameFlow.GameState.MENU;
        GameFlow.pause = false;
        GameGUI.GHPauseMenu.SetActive(false);
        GameGUI.GHMainMenu.SetActive(true);
        transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").gameObject.SetActive(true);
        transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").gameObject.SetActive(false);
    }


    private void Deactivate()
    {
        GameGUI.GHPauseMenu.SetActive(false);
    }
}
