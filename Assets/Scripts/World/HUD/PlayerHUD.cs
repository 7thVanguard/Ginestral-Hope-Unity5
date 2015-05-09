﻿using UnityEngine;
using System.Collections;

public class PlayerHUD
{
    private Texture2D lifeFull;
    private Texture2D lifeEmpty;
    private Texture2D coin;

    private Color orbColor = Color.white;

    private float collectedOrbMargin = 0;
    private int margin = 5;


    public void Start()
    {
        lifeFull = (Texture2D)Resources.Load("UI/HUD/LifeFull");
        lifeEmpty = (Texture2D)Resources.Load("UI/HUD/LifeEmpty");
        coin = (Texture2D)Resources.Load("UI/HUD/Coin");
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


        // Orbs
        float textureSize = Screen.width / 60;

        GUI.skin.label.fontSize = 30;
        GUI.skin.font = (Font)Resources.Load("Fonts/Amigo-Regular");

        // Texture parameters
        if (GameFlow.orbCollected)
        {
            GameFlow.orbCollected = false;
            orbColor.g = 0;
            orbColor.b = 0;
            collectedOrbMargin = textureSize / 2;
        }
        else
        {
            if (orbColor.g < 1)
            {
                orbColor.g += Time.deltaTime / 3;
                orbColor.b += Time.deltaTime / 3;

                collectedOrbMargin = (textureSize / 2) * (1 - orbColor.g);
            }
            else
            {
                orbColor.g = 1;
                orbColor.b = 1;

                collectedOrbMargin = 0;
            }
        }

        GUI.color = orbColor;
        GUI.DrawTextureWithTexCoords(new Rect(Screen.width / 2 - textureSize - collectedOrbMargin / 2, margin - collectedOrbMargin / 2, 2 * textureSize + collectedOrbMargin, 2 * textureSize + collectedOrbMargin), coin, 
                                     new Rect(0, 0, 1, 1));
        GUI.color = Color.white;


        // Draw text
        GUI.Label(new Rect(margin + Screen.width / 2 + Screen.width / 60, margin, Screen.width / 3, Screen.width / 3), 
                  "x " + Global.player.orbsCollected);
    }
}
