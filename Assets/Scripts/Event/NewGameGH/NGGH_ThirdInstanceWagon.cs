using UnityEngine;
using System.Collections;

public class NGGH_ThirdInstanceWagon : MonoBehaviour
{
    public enum CameraTravel { Step1, Step2, Step3 }
    public CameraTravel cameraTravel = CameraTravel.Step1;

    private Animation animator;

    private bool inTrigger = false;
    private bool activated = false;
    private bool finished = false;

    private Quaternion cameraPivotRotation;
    private Vector3 cameraPivotPosition = new Vector3(7.125f, 11, 14);

    private Quaternion cameraInitialRotation;
    private Vector3 cameraInitialPosition;
    private float timeCounter = 0;

	void Start() 
	{
		animator = transform.parent.GetComponent<Animation>();
		animator["Anim01"].speed = 0.5f;
	}


    void Update()
    {
        // Reset
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
        {
            animator["Anim01"].speed = -1;
            animator.Play();
            activated = false;
            finished = false;
        }

        if (activated && !finished)
        {
            switch (cameraTravel)
            {
                case CameraTravel.Step1:
                    {
                        Global.mainCamera.cameraObj.transform.LookAt(transform);
                        Quaternion auxiliarRotation = Global.mainCamera.cameraObj.transform.rotation;

                        Global.mainCamera.cameraObj.transform.position = Vector3.Lerp(cameraInitialPosition, cameraPivotPosition, timeCounter);
                        Global.mainCamera.cameraObj.transform.rotation = Quaternion.Lerp(cameraInitialRotation, auxiliarRotation, timeCounter);

                        if (timeCounter < 1)
                            timeCounter += Time.deltaTime / 2;
                        else
                        {
                            Global.mainCamera.cameraObj.transform.position = cameraPivotPosition;
                            Global.mainCamera.cameraObj.transform.rotation = auxiliarRotation;

                            timeCounter = 0;
                            animator["Anim01"].speed = 0.25f;
                            animator.Play();

                            cameraTravel = CameraTravel.Step2;
                        }
                    }
                    break;
                case CameraTravel.Step2:
                    {
                        Global.mainCamera.cameraObj.transform.LookAt(transform);

                        if (timeCounter < 1)
                            timeCounter += Time.deltaTime / 4;
                        else
                        {
                            cameraPivotRotation = Global.mainCamera.cameraObj.transform.rotation;
                            timeCounter = 0;

                            cameraTravel = CameraTravel.Step3;
                        }
                    }
                    break;
                case CameraTravel.Step3:
                    {
                        Global.mainCamera.cameraObj.transform.LookAt(transform);
                        Quaternion auxiliarRotation = Global.mainCamera.cameraObj.transform.rotation;

                        Global.mainCamera.cameraObj.transform.position = Vector3.Lerp(cameraPivotPosition, cameraInitialPosition, timeCounter);
                        Global.mainCamera.cameraObj.transform.rotation = Quaternion.Lerp(cameraPivotRotation, cameraInitialRotation, timeCounter);

                        if (timeCounter < 1)
                            timeCounter += Time.deltaTime / 2;
                        else
                        {
                            GameFlow.onCameraTravel = false;
                            finished = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        inTrigger = true;

        if (!activated)
            if (other.tag == "Player")
                if (Input.GetKey(KeyCode.E))
                {
                    // Push the MineCart
                    cameraInitialPosition = Global.mainCamera.cameraObj.transform.position;
                    cameraInitialRotation = Global.mainCamera.cameraObj.transform.rotation;

                    GameFlow.onCameraTravel = true;
                    activated = true;
                }
    }


    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }


    void OnGUI()
    {
        if (!activated)
            if (inTrigger)
                EventsLib.DrawInteractivity();
    }
}
