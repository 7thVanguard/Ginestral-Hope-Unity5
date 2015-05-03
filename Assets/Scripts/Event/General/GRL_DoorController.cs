using UnityEngine;
using System.Collections;

public class GRL_DoorController : MonoBehaviour 
{
    World world;

    public GameObject firstDoor;
    public GameObject secondDoor;
    public Vector3 firstDoorObjectivePosition;
    public Vector3 secondDoorObjectivePosition;
    private Vector3 firstDoorBasePosition;
    private Vector3 secondDoorBasePosition;

    public Color ambient;

    public bool activeOnEnter;

    private float timeCounter = 0;
    private bool active;

    private bool inTrigger = false;


    void Start()
    {
        firstDoorBasePosition = firstDoor.transform.position;
        secondDoorBasePosition = secondDoor.transform.position;
    }


    void Update()
    {
        if (active)
        {
            EventsLib.UpdateRenderambient();
            EventsLib.UpdateDoorOpenDoubleSlider();

            timeCounter -= Time.deltaTime;
            if (timeCounter < 0)
                active = false;
        }

        // Reset
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
        {
            active = false;
            firstDoor.transform.position = firstDoorBasePosition;
            secondDoor.transform.position = secondDoorBasePosition;
        }
    }


    void OnTriggerStay(Collider other)
    {
        inTrigger = true;

        if (GameFlow.gameMode == GameFlow.GameMode.PLAYER)
            if (other.tag == "Player")
            {
                if (activeOnEnter)
                {
                    EventsLib.SetRenderambient(ambient);
                    EventsLib.SetDoorOpenDoubleSlider(firstDoor, firstDoorObjectivePosition, secondDoor, secondDoorObjectivePosition);

                    timeCounter = 10;
                    active = true;
                }
                else
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        EventsLib.SetRenderambient(ambient);
                        EventsLib.SetDoorOpenDoubleSlider(firstDoor, firstDoorObjectivePosition, secondDoor, secondDoorObjectivePosition);

                        timeCounter = 10;
                        active = true;
                    }
                }
            }
    }


    void OnTriggerExit()
    {
        inTrigger = false;
    }


    void OnGUI()
    {
        if (!activeOnEnter)
            if (!active)
                if (inTrigger)
                    EventsLib.DrawInteractivity();
    }
}
