using UnityEngine;
using System.Collections;

public class CameraMovement
{
    Player player;
    MainCamera mainCamera;

    RaycastHit impact;


    public CameraMovement(Player player, MainCamera mainCamera)
    {
        this.player = player;
        this.mainCamera = mainCamera;
    }


    public void Start(GameObject camera)
    {
        Vector3 beginingRotation = mainCamera.cameraObj.transform.eulerAngles;
        mainCamera.objectivePosition = beginingRotation.y;
        mainCamera.angleSight = beginingRotation.x;

        Physics.IgnoreCollision(mainCamera.cameraObj.GetComponent<Collider>(), player.playerObj.GetComponent<Collider>());
    }


    public void Update()
    {
        if (GameFlow.gameState == GameFlow.GameState.GAME && !GameFlow.onInterface)
        {
            if (GameFlow.gameMode == GameFlow.GameMode.DEVELOPER)
            {
                mainCamera.distance = 0;
                mainCamera.objectiveDistance = 0;

                // Camera aim depending on the player
                mainCamera.offset = mainCamera.cameraObj.transform.forward / 3 + Vector3.up / 2 + Vector3.up * mainCamera.playerRelativeHeight;

                // Mouse inputs
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    mainCamera.objectivePosition += Input.GetAxis("Mouse X") * mainCamera.mouseSensitivityX;
                    mainCamera.angleSight -= Input.GetAxis("Mouse Y") * mainCamera.mouseSensitivityY;
                }

                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                    mainCamera.isMoving = true;
                else
                    mainCamera.isMoving = false;
            }
            else
            {
                mainCamera.offset = mainCamera.cameraObj.transform.right * 0.5f +
                                    mainCamera.cameraObj.transform.forward * 0.3f +
                                    Vector3.up * mainCamera.playerRelativeHeight +          // Adapt to the player pivot point
                                    Vector3.up * (1 - mainCamera.angleSight / 45.0f);

                // Mouse inputs
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    mainCamera.objectivePosition += Input.GetAxis("Mouse X") * mainCamera.mouseSensitivityX;
                    mainCamera.angleSight -= Input.GetAxis("Mouse Y") * mainCamera.mouseSensitivityY;
                }

                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                    mainCamera.isMoving = true;
                else
                    mainCamera.isMoving = false;

                if (mainCamera.objectiveDistance > mainCamera.maxDistance)
                    mainCamera.objectiveDistance = mainCamera.maxDistance;
                else if (mainCamera.objectiveDistance < mainCamera.maxDistance && (mainCamera.isMoving || player.isMoving))
                    mainCamera.objectiveDistance += 7.5f * Time.deltaTime;

                Screen.lockCursor = true;
            }


            // Zoom adapt
            if (mainCamera.objectiveDistance < mainCamera.distance && (mainCamera.isMoving || player.isMoving))
                mainCamera.objectiveDistance += 7.5f * Time.deltaTime;

            // Angle sight clamp
            mainCamera.angleSight = ClampAngle(mainCamera.angleSight, mainCamera.minAngleSight, mainCamera.maxAngleSight);

            // Camera rotation
            Quaternion rotation = Quaternion.Euler(mainCamera.angleSight, mainCamera.objectivePosition, 0);
            mainCamera.cameraObj.transform.rotation = rotation;

            // Camera position 
            mainCamera.previousPosition = mainCamera.cameraObj.transform.position;
            mainCamera.position = player.playerObj.transform.position + mainCamera.offset - (rotation * Vector3.forward * mainCamera.objectiveDistance);
            mainCamera.interpolatedPosition = Vector3.Lerp(mainCamera.previousPosition, mainCamera.position, 0.75f);
            mainCamera.controller.Move(mainCamera.interpolatedPosition - mainCamera.previousPosition);

            // Blocked view detection
            Vector3 playerInScreenPosition = player.playerObj.transform.position + mainCamera.offset;

            // Raycast with an objective
            if (Physics.Linecast(playerInScreenPosition, mainCamera.cameraObj.transform.position, out impact))
            {
                if (impact.transform.gameObject.tag != "MainCamera" && impact.transform.gameObject.tag != "Player")
                {
                    // Reallocates the camera
                    float advancedDistance = Vector3.Distance(playerInScreenPosition, impact.point) - 0.5f;
                    mainCamera.objectiveDistance = Vector3.Distance(playerInScreenPosition, impact.point) - 0.5f;
                    mainCamera.cameraObj.transform.position = player.playerObj.transform.position + mainCamera.offset - (rotation * Vector3.forward * advancedDistance);
                }
            }
        }
    }


    private float ClampAngle(float angle, float min, float max)
    {
        // Adjust the angle between 0 and 360º
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        // Camera is only TopDown in player mode
        if (GameFlow.gameMode != GameFlow.GameMode.PLAYER)
        {
            min = -90;
            max = 90;
        }

        // Return the angleSight clamped
        return Mathf.Clamp(angle, min, max);
    }
}
