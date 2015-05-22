using UnityEngine;
using System.Collections;

public class GRL_FireDoor : MonoBehaviour 
{
    public GameObject firstBrazier;
    public GameObject secondBrazier;
    public GameObject thirdBrazier;

    [HideInInspector] public bool totalClose;
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
        totalClose = false;

        EventsLib.SetDoorOpenDoubleSlider(lefttDoor, leftDoorBasePosition, rightDoor, rightDoorBasePosition);
	}
	


	void Update ()
    {
        if (!totalClose)
            if (firstBrazier.GetComponent<GRL_FireEmitter>().emitting)
                if (secondBrazier.GetComponent<GRL_FireEmitter>().emitting)
                    if (thirdBrazier.GetComponent<GRL_FireEmitter>().emitting)
                    {
                        if (!open)
                        {
                            if (!audio.isPlaying)
                                audio.Play();

                            EventsLib.SetDoorOpenDoubleSlider(lefttDoor, leftDoorObjectivePosition, rightDoor, rightDoorObjectivePosition);
                            open = true;
                        }
                    }

        if (open || close || totalClose)
        {
            EventsLib.UpdateDoorOpenDoubleSlider();
        }

        if (close || totalClose)
        {
            if (open)
            {
                if (!audio.isPlaying)
                    audio.Play();

                EventsLib.SetDoorOpenDoubleSlider(lefttDoor, leftDoorBasePosition, rightDoor, rightDoorBasePosition);
                open = false;
            }
        }
	}
}
