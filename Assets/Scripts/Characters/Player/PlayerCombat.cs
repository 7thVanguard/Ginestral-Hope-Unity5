﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerCombat
{
    private Player player;
    MainCamera mainCamera;

    // Detection relative
    public static GameObject target;
    public static Vector3 circleGizmoScreenPos;
    private int detectionDistance = 30;


    public PlayerCombat(Player player, MainCamera mainCamera)
    {
        this.player = player;
        this.mainCamera = mainCamera;

        // Detection relative
        target = null;
        circleGizmoScreenPos = Vector3.zero;
    }


    public void Update()
    {
        // Detection relative
        if (mainCamera.raycast.distance < detectionDistance && mainCamera.raycast.distance != 0)
        {
            if (mainCamera.raycast.collider == null) { }
            else if (mainCamera.raycast.collider.CompareTag("Enemy"))
                target = mainCamera.raycast.transform.gameObject;
        }


        if (target != null)
        {
            circleGizmoScreenPos = Camera.main.WorldToScreenPoint(target.transform.position);

            // Abandon target
            if (Vector3.Distance(player.playerObj.transform.position, target.transform.position) > detectionDistance)
                target = null;

            if (Vector2.Distance(new Vector2(circleGizmoScreenPos.x, circleGizmoScreenPos.y), new Vector2(Screen.width / 2, Screen.height / 2)) > Screen.height / 2.5f)
                target = null;
        }
        else
        {
            if (Application.loadedLevelName != "Cavernina")
                if (GameFlow.gameMode == GameFlow.GameMode.PLAYER && GameFlow.gameState == GameFlow.GameState.GAME && !GameFlow.pause && !GameFlow.onInterface && !GameFlow.onCameraTravel)
					if (Input.GetKeyDown(KeyCode.Mouse0) || ((GameManager.padState.Buttons.B == ButtonState.Pressed) && (GameManager.previousPadState.Buttons.B == ButtonState.Released)))
                        if (Global.player.fireCharges > 0)
                        {
                            Skill.Dictionary["Fire Ball"].CastDirected(null, player.playerObj.transform.position + new Vector3(0, 1, 0), true);

                            Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Attack");
                            Global.player.animationCoolDown = 30;
                            Global.player.fireCharges--;
                        }
        }
    }
}
