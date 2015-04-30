using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class GUIGHMainMenu : MonoBehaviour 
{
    GameObject mainMenu;
    GameObject optionsMenu;
    GameObject texturePackMenu;

    GameObject buttonNewGame;
    GameObject buttonOptions;
    GameObject buttonExitGame;

    GameObject blackSpace;

    Texture2D selectedAtlas;

    float menuColor = 0;
    float alphaCounterBlackScreen = 0;

    bool newGame = false;
    bool fadingIn = false;
    bool fadingOut = false;


    void Awake()
    {
        GameGUI.GHMainMenu = transform.parent.FindChild("GH Main Menu").gameObject;

        mainMenu = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").gameObject;
        optionsMenu = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").gameObject;
        texturePackMenu = transform.parent.FindChild("GH Main Menu").FindChild("Texture Pack Menu").gameObject;

        buttonNewGame = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN New Game").gameObject;
        buttonOptions = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Options").gameObject;
        buttonExitGame = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Exit Game").gameObject;

        blackSpace = transform.parent.FindChild("Black Space").gameObject;

        selectedAtlas = (Texture2D)Global.G1.mainTexture;

        mainMenu.SetActive(true);
        texturePackMenu.SetActive(false);
        optionsMenu.SetActive(false);
        blackSpace.SetActive(false);
    }


    void Update()
    {
        // Alpha buttons animation
        if (menuColor < 1)
        {
            menuColor += Time.deltaTime / 4;
            buttonNewGame.GetComponent<Image>().color = new Color(1, 1, 1, menuColor);
            buttonOptions.GetComponent<Image>().color = new Color(1, 1, 1, menuColor);
            buttonExitGame.GetComponent<Image>().color = new Color(1, 1, 1, menuColor);

            buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menuColor);
            buttonOptions.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menuColor);
            buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menuColor);
        }
        else if (menuColor > 1)
        {
            menuColor = 1;
            buttonNewGame.GetComponent<Image>().color = new Color(1, 1, 1, menuColor);
            buttonOptions.GetComponent<Image>().color = new Color(1, 1, 1, menuColor);
            buttonExitGame.GetComponent<Image>().color = new Color(1, 1, 1, menuColor);

            buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menuColor);
            buttonOptions.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menuColor);
            buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menuColor);
        }

        // Fade in and out at new game
        if (newGame)
        {
            if (fadingIn)
            {
                if (alphaCounterBlackScreen < 1)
                    alphaCounterBlackScreen += 1 * Time.deltaTime / 2;
                else
                {
                    alphaCounterBlackScreen = 1;
                    fadingIn = false;
                    fadingOut = true;
                    GameFlow.gameState = GameFlow.GameState.GAME;
                    Deactivate();
                }
            }
            else if (fadingOut)
            {
                if (alphaCounterBlackScreen > 0)
                    alphaCounterBlackScreen -= 1 * Time.deltaTime / 2;
                else
                {
                    alphaCounterBlackScreen = 0;
                    newGame = false;
                    fadingOut = false;
                    blackSpace.SetActive(false);
                }
            }
            
            blackSpace.GetComponent<Image>().color = new Color(0, 0, 0, alphaCounterBlackScreen);
        }
    }


    void OnGUI()
    {
        if (texturePackMenu.activeSelf)
            GUI.DrawTexture(new Rect(Screen.width * 3 / 5, 20, Screen.width * 2 / 5 - 20, Screen.width * 2 / 5 - 20), selectedAtlas);
    }

    // New Game
    public void NewGameButton()
    {
        //Global.world.worldObj.GetComponent<GameManager>().gameSerializer.Load(Global.world, Global.player, "NewGameGH");

        // World preparation
        Global.mainCamera.cameraObj.GetComponent<Camera>().renderingPath = RenderingPath.DeferredLighting;

        foreach (Transform child in Global.world.worldObj.transform)
            GameObject.Destroy(child.gameObject);

        GameObject.Destroy(Global.world.geometryController);
        GameObject.Destroy(Global.world.eventsController);
        GameObject.Destroy(Global.world.interactivesController);
        GameObject.Destroy(Global.world.enemiesController);
        GameObject.Destroy(Global.world.emitersController);

        // Player
        Global.player.playerObj.transform.position = new Vector3(34.5f, 1, 17);
        Global.player.playerObj.transform.eulerAngles = new Vector3(0, 160, 0);

        // Camera
        Global.mainCamera.cameraObj.GetComponent<Camera>().backgroundColor = new Color32(255, 166, 71, 255);

        // Sun
        Global.sun.sunObj.transform.position = new Vector3(13, 30, 100);
        Global.sun.sunObj.transform.eulerAngles = new Vector3(14.59f, 171, 1.1f);
        Global.sun.sunObj.transform.GetComponent<Light>().color = new Color32(230, 174, 71, 255);
        Global.sun.lensFlare.color = new Color32(255, 141, 0, 255);
        Global.sun.sunObj.transform.GetComponent<Light>().color = new Color32(230, 174, 71, 255);

        // Renderer
        RenderSettings.ambientLight = new Color32(43, 45, 37, 255);

        // Fade in and out
        newGame = true;
        fadingIn = true;
        blackSpace.SetActive(true);
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }


    public void LoadGameButton()
    {
        GameFlow.gameState = GameFlow.GameState.GAME;
        Deactivate();
    }


    public void DeveloperButton()
    {
        GameFlow.gameState = GameFlow.GameState.GAME;
        Deactivate();
    }


    public void TexturePackButton()
    {
        mainMenu.SetActive(false);
        texturePackMenu.SetActive(true);
    }


    public void ExitGame()
    {
        Application.Quit();
    }


    // Texture Pack
    public void Ginestral1Button()
    {
        GameFlow.selectedAtlas = Global.G1;
        selectedAtlas = (Texture2D)Global.G1.mainTexture;
        ResetWorld();
    }


    public void Planned1Button()
    {
        GameFlow.selectedAtlas = Global.P1;
        selectedAtlas = (Texture2D)Global.P1.mainTexture;
        ResetWorld();
    }


    // General
    public void BackButton()
    {
        mainMenu.SetActive(true);
        texturePackMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }


    // Internal
    private void ResetWorld()
    {
        // Destroy existing emiters
        GameObject[] chunks = GameObject.FindGameObjectsWithTag("Chunk");
        foreach (GameObject chunkObj in chunks)
            GameObject.Destroy(chunkObj);

        Global.world.Init();
    }


    public void SliderMasterVolume()
    {
        GameMusic.masterVolume = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Master Volume").GetComponent<Slider>().value;
    }


    public void SliderMusicVolume()
    {
        GameMusic.musicVolume = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Music Volume").GetComponent<Slider>().value;
    }
    

    public void SliderAmbientVolume()
    {
        GameMusic.ambientVolume = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Ambient Volume").GetComponent<Slider>().value;
    }


    public void SliderFXVolume()
    {
        GameMusic.FXVolume = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD FX Volume").GetComponent<Slider>().value;
    }
    
    
    public void SliderGraphicsQuality()
    {
        QualitySettings.SetQualityLevel((int)transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Graphics Quality").GetComponent<Slider>().value);
    }


    private void Deactivate()
    {
        GameGUI.GHMainMenu.SetActive(false);
    }
}
