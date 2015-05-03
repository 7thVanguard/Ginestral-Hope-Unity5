using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class GameManager : MonoBehaviour
{
    // Controllers
    private Dictionary<string, GameController> Controller;

    private GameController activeController;


    // Main control classes
    private Enemy enemy;
    private Skill skill;
    private GamePadState padState;
    private PlayerIndex padIndex;

    // Save
    public GameSerializer gameSerializer;
    public string saveName;

    void Awake()
    {
        // Basic variables, activated in PreInit
        VoxelsList.Init();                                                                                  // Initialize Voxels


        //+ Object Init
        Global.world = new World(gameObject);
        Global.player = new Player(Global.world);
        Global.mainCamera = new MainCamera();
        Global.sun = new Sun(Global.player);

        // post initialize
        Global.world.worldObj.GetComponent<HUD>().Init(Global.player);                                      // Initialize HUD
        VoxelsList.PostInit(Global.world, Global.mainCamera);


        //+ Controllers Init
        Controller = new Dictionary<string, GameController>();


        //+ Physic ignores
        Physics.IgnoreCollision(Global.mainCamera.cameraObj.GetComponent<Collider>(), Global.player.playerObj.GetComponent<CharacterController>());
        Physics.IgnoreCollision(Global.player.playerObj.GetComponent<CharacterController>(), Global.mainCamera.cameraObj.GetComponent<Collider>());


        //+ Game modes
        GameController aPC;

        aPC = new PlayerGameController(Global.world, Global.player, Global.mainCamera);
        aPC.Start();
        Controller.Add("PlayerMode", aPC);

        aPC = new GodGameController(Global.world, Global.player, Global.mainCamera);
        aPC.Start();
        Controller.Add("GodMode", aPC);

        aPC = new DeveloperGameController(Global.world, Global.player, Global.mainCamera, Global.sun);
        aPC.Start();
        Controller.Add("DeveloperMode", aPC);

        activeController = Controller["PlayerMode"];

        //+ Enemies Init
        enemy = new Enemy();
        enemy.Init(Global.world, Global.player, Global.mainCamera, enemy);

        //+ Skills Init
        skill = new Skill();
        skill.Init(Global.world, Global.player, Global.mainCamera, skill);

        gameSerializer = new GameSerializer();

        // Music Init
        GameMusic.Init();
    }


    void Update()
    {
        // Gamepad
        padState = GamePad.GetState(padIndex);

        if (GameFlow.gameState == GameFlow.GameState.MENU)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (GameFlow.gameState == GameFlow.GameState.GAME)
        {
            //+ Global inputs
            // Game mode
            if (Input.GetKey(KeyCode.F1))
            {
                activeController = Controller["PlayerMode"];
                PassToPlayer();
            }
            else if (Input.GetKey(KeyCode.F2))
            {
                activeController = Controller["GodMode"];
                PassToGod();
            }
            else if (Input.GetKey(KeyCode.F3))
            {
                activeController = Controller["DeveloperMode"];
                PassToDeveloper();
            }
                

            // Save relative
            if (Input.GetKeyUp(KeyCode.F5))
                gameSerializer.Save(Global.world, Global.player, saveName);
            else if (Input.GetKeyUp(KeyCode.F9))
                gameSerializer.Load(Global.world, Global.player, saveName);
            else if (Input.GetKeyUp(KeyCode.F10))
            {
                gameSerializer.Save(Global.world, Global.player, "Reload");
                gameSerializer.Load(Global.world, Global.player, "Reload");
            }


            //+ Controllers
            activeController.Update();

            if ((Input.GetKeyUp(KeyCode.Escape)) || ((padState.IsConnected) && (padState.Buttons.Start == ButtonState.Released)))
            {
                if (!GameFlow.onInterface)
                    GameFlow.pause = !GameFlow.pause;

                if(GameFlow.pause)
                    GameGUI.GHPauseMenu.SetActive(true);
                else
                    GameGUI.GHPauseMenu.SetActive(false);
            }

            // Cursor control
            if (GameFlow.onInterface || GameFlow.pause)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                if (GameFlow.gameMode != GameFlow.GameMode.PLAYER)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    else
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
        }

        // Generic Updates
        GameMusic.Update();


        //+ Reset queue
        if (Global.world.chunksToReset.Count > 0)
        {
            // Check if there is more than one chunk to reset, and reset the second in the list if true
            if (Global.world.chunksToReset.Count > 1)
            {
                // Resets the chunk
                Global.world.chunk[Global.world.chunksToReset[1].x, Global.world.chunksToReset[1].y, Global.world.chunksToReset[1].z].BuildChunkVertices(Global.world);
                Global.world.chunk[Global.world.chunksToReset[1].x, Global.world.chunksToReset[1].y, Global.world.chunksToReset[1].z].BuildChunkMesh();

                // Removes the reseted chunk from the list
                Global.world.chunksToReset.Remove(Global.world.chunksToReset[1]);
            }

            // Resets the chunk
            Global.world.chunk[Global.world.chunksToReset[0].x, Global.world.chunksToReset[0].y, Global.world.chunksToReset[0].z].BuildChunkVertices(Global.world);
            Global.world.chunk[Global.world.chunksToReset[0].x, Global.world.chunksToReset[0].y, Global.world.chunksToReset[0].z].BuildChunkMesh();

            // Removes the reseted chunk from the list
            Global.world.chunksToReset.Remove(Global.world.chunksToReset[0]);
        }

        // Reset gameplay
        if (GameFlow.resetState == GameFlow.ResetState.New)
            GameFlow.resetState = GameFlow.ResetState.Reset;
        else if (GameFlow.resetState == GameFlow.ResetState.Reset)
            GameFlow.resetState = GameFlow.ResetState.End;
    }


    private void PassToPlayer()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("GameObject"), false);

        GameFlow.gameMode = GameFlow.GameMode.PLAYER;
        Global.player.constructionDetection = 300;

        HUD.cubeMarker.SetActive(false);

        // UI
        GameGUI.developerMode.SetActive(false);
    }


    private void PassToGod()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("GameObject"), true);

        GameFlow.gameMode = GameFlow.GameMode.GODMODE;
        Global.player.constructionDetection = 300;

        HUD.cubeMarker.SetActive(false);

        // UI
        GameGUI.developerMode.SetActive(false);
    }


    private void PassToDeveloper()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("GameObject"), false);

        GameFlow.gameMode = GameFlow.GameMode.DEVELOPER;
        Global.player.constructionDetection = 300;

        if (GameFlow.selectedTool == GameFlow.SelectedTool.VOXEL)
            HUD.cubeMarker.SetActive(true);
        else
            HUD.cubeMarker.SetActive(false);

        // UI
        GameGUI.developerMode.SetActive(true);
    }


    private void ChanmgeGameParams()
    {
        Global.mainCamera.ChangeCameraParams();
    }
}