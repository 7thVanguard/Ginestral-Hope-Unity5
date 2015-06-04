using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIGHMainMenu : MonoBehaviour 
{
    GameObject mainMenu;
    GameObject optionsMenu;
    GameObject texturePackMenu;
    GameObject creditsMenu;
    GameObject selecteLevelMenu;

    GameObject buttonNewGame;
    GameObject buttonOptions;
    GameObject buttonLevel;
    GameObject buttonExitGame;

    GameObject blackSpace;
    GameObject waterMark;

    AudioSource audioSource;
    AudioSource movieSource;

    MovieTexture movie;
    Texture2D selectedAtlas;

    Color menuColor = Color.black;
    float menualphaColor = 0;
    float alphaCounterBlackScreen = 0;
    float movieTimeCounter;

    // Menu control
    bool newGame = false;
    bool fadingIn = false;
    bool moviePlaying = false;
    bool loading = false;
    bool fadingOut = false;

    bool pointerInNewButton = false;
    bool pointerInOptionsButton = false;
    bool pointerInLevelButton = false;
    bool pointerInExitButton = false;


    void Awake()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        GameGUI.GHMainMenu = transform.parent.FindChild("GH Main Menu").gameObject;

        audioSource = transform.parent.GetComponent<AudioSource>();
        movieSource = transform.GetComponent<AudioSource>();

        // Planes
        mainMenu = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").gameObject;
        optionsMenu = transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").gameObject;
        texturePackMenu = transform.parent.FindChild("GH Main Menu").FindChild("Texture Pack Menu").gameObject;
        creditsMenu = transform.parent.FindChild("GH Main Menu").FindChild("Credits Menu").gameObject;
        selecteLevelMenu = transform.parent.FindChild("GH Main Menu").FindChild("Select Level Menu").gameObject;

        // Buttons
        buttonNewGame = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN New Game").gameObject;
        buttonOptions = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Options").gameObject;
        buttonLevel = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Select Level").gameObject;
        buttonExitGame = transform.parent.FindChild("GH Main Menu").FindChild("Main Menu").FindChild("BTN Exit Game").gameObject;
        

        blackSpace = transform.parent.FindChild("Black Space").gameObject;
        waterMark = transform.parent.FindChild("WaterMark").gameObject;

        movie = (MovieTexture)Resources.Load("Cinematics/Cinematic01_English");
        movieTimeCounter = 0;
        selectedAtlas = (Texture2D)Global.MineAtlas.mainTexture;

        // Sliders
        transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Graphics Quality").GetComponent<Slider>().value = QualitySettings.GetQualityLevel();
        transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD Mouse Sensitivity").GetComponent<Slider>().value = Global.mainCamera.mouseSensitivityX;
        transform.parent.FindChild("GH Main Menu").FindChild("Options Menu").FindChild("SLD FX Volume").GetComponent<Slider>().value = 0.8f;

        mainMenu.SetActive(true);
        texturePackMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        selecteLevelMenu.SetActive(false);
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
                    moviePlaying = true;
                    movie.Play();
                    movieSource.Play();
                    Global.player.playerObj.transform.FindChild("MusicPlayer").gameObject.SetActive(false);

                    // Prepare for the options pause menu
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    texturePackMenu.SetActive(false);
                    creditsMenu.SetActive(false);
                    selecteLevelMenu.SetActive(false);
                    Deactivate();
                }
            }
            else if (moviePlaying)
            {
                movieTimeCounter -= Time.deltaTime;

                if (Input.GetKey(KeyCode.Space))
                    movieTimeCounter = 0;

                if (movieTimeCounter <= 0)
                {
                    Global.player.playerObj.transform.FindChild("MusicPlayer").gameObject.SetActive(true);
                    movieSource.Stop();
                    movie.Stop();

                    loading = true;
                    moviePlaying = false;

                    GameFlow.gameState = GameFlow.GameState.GAME;
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
                buttonLevel.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menualphaColor);
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color = new Color(0, 0, 0, menualphaColor);
            }
            else if (menualphaColor < 1)
            {
                menualphaColor += Time.deltaTime / 4;
                buttonNewGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonOptions.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonLevel.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonExitGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);

                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonOptions.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonLevel.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonOptions.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, menualphaColor - buttonExitGame.transform.FindChild("Text").GetComponent<Text>().color.a);
            }
            else if (menualphaColor > 1)
            {
                menualphaColor = 1;
                buttonNewGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonOptions.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonLevel.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);
                buttonExitGame.GetComponent<Image>().color = new Color(1, 1, 1, menualphaColor);

                buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, 1 - buttonNewGame.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonOptions.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, 1 - buttonOptions.transform.FindChild("Text").GetComponent<Text>().color.a);
                buttonLevel.transform.FindChild("Text").GetComponent<Text>().color += new Color(0, 0, 0, 1 - buttonOptions.transform.FindChild("Text").GetComponent<Text>().color.a);
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

            if (pointerInLevelButton)
            {
                buttonLevel.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonLevel.transform.FindChild("Text").GetComponent<Text>().color, new Color32(80, 40, 10, (byte)(255 * menualphaColor)), 0.1f);
                buttonLevel.transform.FindChild("Text").localScale = Vector3.Lerp(buttonLevel.transform.FindChild("Text").localScale, new Vector3(1.2f, 1.2f, 1), 0.1f);
            }
            else
            {
                buttonLevel.transform.FindChild("Text").GetComponent<Text>().color = Color.Lerp(buttonLevel.transform.FindChild("Text").GetComponent<Text>().color, new Color(0, 0, 0, menualphaColor), 0.1f);
                buttonLevel.transform.FindChild("Text").localScale = Vector3.Lerp(buttonLevel.transform.FindChild("Text").localScale, Vector3.one, 0.1f);
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

        if (movie.isPlaying)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie);
    }


    // New Game
    public void NewGameButton()
    {
        audioSource.Play();
        Application.LoadLevel(2);

        newGame = true;
        fadingIn = true;

        blackSpace.SetActive(true);
        movieTimeCounter = movie.duration;
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

    public void OnPointerEnterLevelButton()
    {
        pointerInLevelButton = true;
    }


    public void OnPointerExitLevelButton()
    {
        pointerInLevelButton = false;
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
        audioSource.Play();

        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }


    public void SelectLevelButton()
    {
        audioSource.Play();

        mainMenu.SetActive(false);
        selecteLevelMenu.SetActive(true);
    }


    public void LoadGameButton()
    {
        audioSource.Play();

        GameFlow.gameState = GameFlow.GameState.GAME;
        Deactivate();
    }


    public void DeveloperButton()
    {
        audioSource.Play();

        Application.LoadLevel(1);
        GameFlow.gameState = GameFlow.GameState.GAME;
        GameFlow.gameMode = GameFlow.GameMode.DEVELOPER;
        RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f, 1);
        Deactivate();
    }


    public void TexturePackButton()
    {
        audioSource.Play();

        mainMenu.SetActive(false);
        texturePackMenu.SetActive(true);
    }


    public void ExitGame()
    {
        audioSource.Play();

        Application.Quit();
    }


    // Texture Pack
    public void Ginestral1Button()
    {
        audioSource.Play();

        GameFlow.selectedAtlas = Global.MineAtlas;
        selectedAtlas = (Texture2D)Global.MineAtlas.mainTexture;
        ResetWorld();
    }


    public void Planned1Button()
    {
        audioSource.Play();

        GameFlow.selectedAtlas = Global.FireAtlas;
        selectedAtlas = (Texture2D)Global.FireAtlas.mainTexture;
        ResetWorld();
    }


    // Levels
    public void CaverninaButton()
    {
        audioSource.Play();
        Application.LoadLevel(2);

        newGame = true;
        fadingIn = true;

        blackSpace.SetActive(true);
    }


    public void CatacombsButton()
    {
        audioSource.Play();
        Application.LoadLevel(3);

        newGame = true;
        fadingIn = true;

        blackSpace.SetActive(true);
    }


    // General
    public void BackButton()
    {
        audioSource.Play();

        if (GameFlow.gameState == GameFlow.GameState.MENU)
        {
            mainMenu.SetActive(true);
            texturePackMenu.SetActive(false);
            optionsMenu.SetActive(false);
            selecteLevelMenu.SetActive(false);
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
        audioSource.Play();

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
        //Global.player.currentLife = Global.player.maxLife;
        Global.player.fireCharges = 3;
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
