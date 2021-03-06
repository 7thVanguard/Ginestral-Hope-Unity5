﻿using UnityEngine;
using System.Collections;

public class PlayerGameController : GameController
{
    private World world;
    private Player player;
    private MainCamera mainCamera;


    public PlayerGameController(World world, Player player, MainCamera mainCamera) : base(world, player, mainCamera)
    {
        this.world = world;
        this.player = player;
        this.mainCamera = mainCamera;
    }


    public override void Start() 
    {
        inputController = new PlayerInputController(world, player, mainCamera);
        base.Start();
	}



    public override void Update() 
    {
        base.Update();

        if (!GameFlow.pause && !GameFlow.onInterface && !GameFlow.onCameraTravel)
        {
            if (player.currentLife > 0)
            {
                playerMovement.NormalMovementUpdate();
                playerCombat.Update();
            }

            cameraMovement.Update();
            cameraRaycast.Update();
        }
	}
}
