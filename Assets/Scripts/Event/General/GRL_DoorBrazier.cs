﻿using UnityEngine;
using System.Collections;

public class GRL_DoorBrazier : MonoBehaviour 
{
    public GameObject firstBrazier;
    public GameObject secondBrazier;
    public GameObject thirdBrazier;

    public bool close;
    public bool open;

    private GameObject lefttDoor;
    private GameObject rightDoor;

    private Vector3 leftDoorBasePosition;
    private Vector3 rightDoorBasePosition;
    private Vector3 leftDoorObjectivePosition;
    private Vector3 rightDoorObjectivePosition;

    private AudioSource audio;


	void Start () 
    {
        lefttDoor = transform.FindChild("LeftDoor").gameObject;
        rightDoor = transform.FindChild("RightDoor").gameObject;

        leftDoorBasePosition = Vector3.zero;
        rightDoorBasePosition = Vector3.zero;
        leftDoorObjectivePosition = new Vector3(-2, 0, 0);
        rightDoorObjectivePosition = new Vector3(2, 0, 0);

        audio = transform.GetComponent<AudioSource>();

        open = false;
	}
	


	void Update ()
    {
        if (firstBrazier.GetComponent<GRL_FireEmitter>().emitting)
            if (secondBrazier.GetComponent<GRL_FireEmitter>().emitting)
                if (thirdBrazier.GetComponent<GRL_FireEmitter>().emitting)
                {
                    if (!open)
                    {
                        audio.Play();
                        EventsLib.SetDoorOpenDoubleSlider(lefttDoor, leftDoorObjectivePosition, rightDoor, rightDoorObjectivePosition);
                        open = true;
                    }
                }

        if (open || close)
        {
            EventsLib.UpdateDoorOpenDoubleSlider();
        }

        if (close)
        {
            if (open)
            {
                audio.Play();
                EventsLib.SetDoorOpenDoubleSlider(lefttDoor, leftDoorBasePosition, rightDoor, rightDoorBasePosition);
                open = false;
            }
        }
	}
}
