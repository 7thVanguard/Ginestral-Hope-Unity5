using UnityEngine;
using System.Collections;

public class PlayerHUD
{
    private Texture2D lifeFull;
    private Texture2D fireFull;
    private Texture2D lifeEmpty;
    private Texture2D coin;

    private Texture2D gizmoCross;

    private Color orbColor = Color.green;

    private float collectedOrbMargin = 0;
    private int margin = 5;

    private bool orbColorGrowing = false;


    public void Start()
    {
        lifeFull = (Texture2D)Resources.Load("UI/HUD/LifeFull");
        fireFull = (Texture2D)Resources.Load("UI/HUD/Fire");
        lifeEmpty = (Texture2D)Resources.Load("UI/HUD/LifeEmpty");
        coin = (Texture2D)Resources.Load("UI/HUD/Coin");

        gizmoCross = TextureCreator.CreateCross(gizmoCross);
    }


    public void Update()
    {
        // Life
        for (int i = 0; i < Global.player.maxLife; i++)
        {
            if (Global.player.currentLife > i)
                GUI.DrawTextureWithTexCoords(new Rect(margin * (i + 1) + i * Screen.width / 40, margin, Screen.width / 40, Screen.width / 40), lifeFull, 
                                             new Rect(0, 0, 1, 1));
            else
                GUI.DrawTextureWithTexCoords(new Rect(margin * (i + 1) + i * Screen.width / 40, margin, Screen.width / 40, Screen.width / 40), lifeEmpty, 
                                             new Rect(0, 0, 1, 1));
        }

        // Fire
        if (Application.loadedLevelName != "Cavernina")
            for (int i = 0; i < Global.player.fireCharges; i++)
                GUI.DrawTextureWithTexCoords(new Rect((Screen.width - fireFull.width) - (margin * (i + 1) + i * Screen.width / 40), (Screen.height - fireFull.height / 2) - margin, Screen.width / 40, Screen.width / 40), fireFull,
                                                 new Rect(0, 0, 1, 1));


        // Orbs
        float textureSize = Screen.width / 60;

        GUI.skin.label.fontSize = 30;
        GUI.skin.font = (Font)Resources.Load("Fonts/Amigo-Regular");

        // Texture parameters
        if (GameFlow.orbCollected)
        {
            GameFlow.orbCollected = false;
            orbColor = Color.yellow;
            collectedOrbMargin = textureSize / 2;

            orbColorGrowing = true;
        }
        else
        {
            if (orbColorGrowing)
            {
                orbColor = Color.Lerp(orbColor, Color.green, 0.002f);
                collectedOrbMargin = (textureSize / 2) * orbColor.r;

                if (orbColor.r < 0.01f)
                    orbColorGrowing = true;
            }
            else
            {
                orbColor = Color.green;

                collectedOrbMargin = 0;
            }
        }

        GUI.color = orbColor;
        GUI.DrawTextureWithTexCoords(new Rect(Screen.width / 2 - textureSize - collectedOrbMargin / 2, margin - collectedOrbMargin / 2, 2 * textureSize + collectedOrbMargin, 2 * textureSize + collectedOrbMargin), coin, 
                                     new Rect(0, 0, 1, 1));
        GUI.color = Color.white;


        // Draw text
        GUI.Label(new Rect(margin + Screen.width / 2 + Screen.width / 60, margin, Screen.width / 3, Screen.width / 3), 
                  "x " + Global.player.orbsCollected + " / 3");

        // Draw Gizmos
        if (GameFlow.gameMode == GameFlow.GameMode.PLAYER && GameFlow.gameState == GameFlow.GameState.GAME && !GameFlow.pause && !GameFlow.onInterface && !GameFlow.onCameraTravel)
            GUI.DrawTexture(new Rect(Screen.width / 2 - gizmoCross.width / 2, Screen.height / 2 - gizmoCross.height, gizmoCross.width, gizmoCross.height), gizmoCross);
    }
}
