using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIGHMainMenu : MonoBehaviour 
{
    GameObject mainMenu;
    GameObject optionsMenu;
    GameObject texturePackMenu;
    GameObject creditsMenu;

    GameObject buttonNewGame;
    GameObject buttonOptions;
    GameObject buttonExitGame;

    GameObject blackSpace;
    GameObject waterMark;

    Texture2D selectedAtlas;

    Color menuColor = Color.black;
    float menualphaColor = 0;
    float alphaCounterBlackScreen = 0;

    // Menu control
    bool newGame = false;
    bool fadingIn = false;
    bool loading = false;
    bool fadingOut = false;

    bool pointerInNewButton = false;
    bool pointerInOptionsButton = false;
    bool pointerInExitButton = false;


    void Awake()
    {
        GameGUI.GHMainMenu = transform.parent.FindChild("GH Main Menu").gameObject;

        // Planes
        mainMenu = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").gameObject;
        optionsMenu = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").gameObject;
        texturePackMenu = transform.parent.FindChild("GH Main Menu").FindChild("Texture Pack Menu").gameObject;
        creditsMenu = transform.parent.FindChild("GH Main Menu").FindChild("Credits Menu").gameObject;

        // Buttons
        buttonNewGame = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN New Game").gameObject;
        buttonOptions = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Options").gameObject;
        buttonExitGame = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Exit Game").gameObject;

        blackSpace = transform.parent.FindChild("Black Space").gameObject;
        waterMark = transform.parent.FindChild("WaterMark").gameObject;

        selectedAtlas = (Texture2D)Global.G1.mainTexture;

        // Sliders
        transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Graphics Quality").GetComponent<Slider>().value = QualitySettings.GetQualityLevel();
        transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Mouse Sensitivity").GetComponent<Slider>().value = Global.mainCamera.mouseSensitivityX;

        mainMenu.SetActive(true);
        texturePackMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        blackSpace.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            newGame = true;
            fadingIn = true;

            blackSpace.SetActive(true);
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
                    loading = true;

                    // Prepare for the options pause menu
                    GameFlow.gameState = GameFlow.GameState.GAME;
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    texturePackMenu.SetActive(false);
                    creditsMenu.SetActive(false);
                    Deactivate();
                }
            }
            else if (loading)
            {
                PrepareNewGame();

                loading = false;
                fadingOut = true;
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

        // Update only if we are int he main menu
        if (GameGUI.GHMainMenu.activeSelf)
        {
            // Alpha buttons animation
            // Initial fade
            if (menualphaColor == 0)
            {
                menualphaColor += Time.deltaTime / 4;
                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menualphaColor);
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menualphaColor);
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menualphaColor);
            }
            else if (menualphaColor < 1)
            {
                menualphaColor += Time.deltaTime / 4;
                buttonNewGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonOptions.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonExitGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);

                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonOptions.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color.a);
            }
            else if (menualphaColor > 1)
            {
                menualphaColor = 1;
                buttonNewGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonOptions.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonExitGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);

                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, 1 - buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, 1 - buttonOptions.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, 1 - buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color.a);
            }


            // Button events
            if (pointerInNewButton)
            {
                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color, new Color32(80, 40, 10, (byte)(255 * menualphaColor)), 0.1f);
                buttonNewGame.transform.FindChild("Text").localScale = Vector3.Lerp(buttonNewGame.transform.FindChild("Text").localScale, new Vector3(1.2f, 1.2f, 1), 0.1f);
            }
            else
            {
                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color, new Color(0, 0, 0, menualphaColor), 0.1f);
                buttonNewGame.transform.FindChild("Text").localScale = Vector3.Lerp(buttonNewGame.transform.FindChild("Text").localScale, Vector3.one, 0.1f);
            }

            if (pointerInOptionsButton)
            {
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonOptions.transform.FindChild("Text").GetComponent<Text>().color, new Color32(80, 40, 10, (byte)(255 * menualphaColor)), 0.1f);
                buttonOptions.transform.FindChild("Text").localScale = Vector3.Lerp(buttonOptions.transform.FindChild("Text").localScale, new Vector3(1.2f, 1.2f, 1), 0.1f);
            }
            else
            {
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonOptions.transform.FindChild("Text").GetComponent<Text>().color, new Color(0, 0, 0, menualphaColor), 0.1f);
                buttonOptions.transform.FindChild("Text").localScale = Vector3.Lerp(buttonOptions.transform.FindChild("Text").localScale, Vector3.one, 0.1f);
            }

            if (pointerInExitButton)
            {
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color, new Color32(80, 40, 10, (byte)(255 * menualphaColor)), 0.1f);
                buttonExitGame.transform.FindChild("Text").localScale = Vector3.Lerp(buttonExitGame.transform.FindChild("Text").localScale, new Vector3(1.2f, 1.2f, 1), 0.1f);
            }
            else
            {
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color, new Color(0, 0, 0, menualphaColor), 0.1f);
                buttonExitGame.transform.FindChild("Text").localScale = Vector3.Lerp(buttonExitGame.transform.FindChild("Text").localScale, Vector3.one, 0.1f);
            }
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
        newGame = true;
        fadingIn = true;

        blackSpace.SetActive(true);
    }

    public void OnPointerEnterNewButton()
    {
        pointerInNewButton = true;
    }

    public void OnPointerExitNewButton()
    {
        pointerInNewButton = false;
    }

    public void OnPointerEnterOptionsButton()
    {
        pointerInOptionsButton = true;
    }

    public void OnPointerExitOptionsButton()
    {
        pointerInOptionsButton = false;
    }

    public void OnPointerEnterExitButton()
    {
        pointerInExitButton = true;
    }

    public void OnPointerExitExitButton()
    {
        pointerInExitButton = false;
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
        if (GameFlow.gameState == GameFlow.GameState.MENU)
        {
            mainMenu.SetActive(true);
            texturePackMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
        else
        {
            GameGUI.GHMainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }
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


    public void WaterMarkButton()
    {
        if (waterMark.activeSelf == true)
            waterMark.SetActive(false);
        else
            waterMark.SetActive(true);
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


    public void SliderMouseSensitivity()
    {
        Global.mainCamera.mouseSensitivityX = (int)transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Mouse Sensitivity").GetComponent<Slider>().value;
        Global.mainCamera.mouseSensitivityX = (int)transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Mouse Sensitivity").GetComponent<Slider>().value;
    }


    private void PrepareNewGame()
    {
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
        Global.player.currentLife = Global.player.maxLife;
        Global.player.orbsCollected = 0;

        Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Idle");

        // Camera
        Global.mainCamera.cameraObj.GetComponent<Camera>().backgroundColor = new Color32(255, 166, 71, 255);

        // Sun
        Global.sun.sunObj.transform.position = new Vector3(13, 30, 100);
        Global.sun.sunObj.transform.eulerAngles = new Vector3(14.59f, 171, 1.1f);
        Global.sun.sunObj.transform.GetComponent<Light>().color = new Color32(230, 174, 71, 255);
        Global.sun.lensFlare.color = new Color32(255, 141, 0, 255);
        Global.sun.sunObj.transform.GetComponent<Light>().color = new Color32(230, 174, 71, 255);
        Global.sun.sunObj.transform.GetComponent<Light>().enabled = false;

        // Renderer
        RenderSettings.ambientLight = new Color32(43, 45, 37, 255);

        // Music
        GameMusic.fadingOut = true;
        GameMusic.fadingIn = false;

        // Reset
        GameFlow.resetState = GameFlow.ResetState.New;
        GameFlow.readyToReset = true;
    }


    private void Deactivate()
    {
        GameGUI.GHMainMenu.SetActive(false);
    }
}
