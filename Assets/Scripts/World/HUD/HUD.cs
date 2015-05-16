using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
    Player player;

    // Delegate classes
    PlayerHUD playerHUD = new PlayerHUD();
    DeveloperHUD developerHUD = new DeveloperHUD();

    // gizmos
    public static Texture2D gizmoCircle;
    private Texture2D gizmoCross;
    private Texture2D translucentSelector;

    // Bars
    private Texture2D lifeBar;
    private Texture2D lifeBarBack;

    // GameObjects
    public static GameObject quadMarker;
    public static GameObject cubeMarker;
    public static GameObject sphereMarker;


    // GUI booleans


    // Helpers
    private Vector2 textureSelection = new Vector2((Screen.height * 5 / 8) / 8, Screen.height * 3 / 8);


    public void Init(Player player)
    {
        this.player = player;

        developerHUD.Init();
    }


	void Awake () 
    {
        playerHUD.Start();

        gizmoCross = TextureCreator.CreateCross(gizmoCross);
        gizmoCircle = TextureCreator.CreateCircle(gizmoCircle);

        translucentSelector = TextureCreator.CreatePixel(translucentSelector, new Color(50.0f / 255, 100.0f / 255, 150.0f / 255, 120.0f / 255));    // translucent blue
        lifeBar = TextureCreator.CreatePixel(lifeBar, Color.green);
        lifeBarBack = TextureCreator.CreatePixel(lifeBarBack, Color.red);

        quadMarker = MarkersCreator.CreateQuadMarker(quadMarker, translucentSelector);
        cubeMarker = MarkersCreator.CreateCubeMarker(cubeMarker, translucentSelector);
        sphereMarker = MarkersCreator.CreateSphereMarker(sphereMarker, translucentSelector);

        DontDestroyOnLoad(cubeMarker);
	}


	void OnGUI ()
    {
        if (GameFlow.gameState == GameFlow.GameState.GAME)
        {
            switch (GameFlow.gameMode)
            {
                //++ Player HUD
                case GameFlow.GameMode.PLAYER:
                    {
                        playerHUD.Update();
                    }
                    break;
                //++ GodMode HUD
                case GameFlow.GameMode.GODMODE:
                    break;
                //++ Developer HUD
                case GameFlow.GameMode.DEVELOPER:
                    {
                        developerHUD.Update();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
