using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRL_Lever : MonoBehaviour 
{
    public List<GameObject> List = new List<GameObject>();

    public bool emitting = false;
    private bool keepPressed = false;
    private bool inTrigger = false;


    void Start()
    {
        if (emitting)
            transform.FindChild("Pull").localRotation = Quaternion.Euler(45, 0, 0);
        else
            transform.FindChild("Pull").localRotation = Quaternion.Euler(315, 0, 0);
    }


    void Update()
    {
        // Reset
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
            emitting = false;

        if (!GameFlow.pause)
        {
            if (emitting)
                transform.FindChild("Pull").localRotation = Quaternion.Lerp(transform.FindChild("Pull").localRotation, Quaternion.Euler(45, 0, 0), 0.1f);
            else
                transform.FindChild("Pull").localRotation = Quaternion.Lerp(transform.FindChild("Pull").localRotation, Quaternion.Euler(315, 0, 0), 0.1f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        inTrigger = true;

        if (other.tag == "Player")
        {
            if (other.GetComponent<CharacterController>().isGrounded)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (!keepPressed)
                    {
                        emitting = !emitting;
                        keepPressed = true;
                        transform.GetComponent<AudioSource>().Play();
                        Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Interact");
                        Global.player.animationCoolDown = 40;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.E))
                    keepPressed = false;


                if (emitting)
                    foreach (GameObject go in List)
                        go.SetActive(true);
                else
                    foreach (GameObject go in List)
                        go.SetActive(false);
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        inTrigger = false;

        if (GameFlow.gameMode == GameFlow.GameMode.PLAYER)
            if (other.tag == "Player")
                keepPressed = false;

        if (emitting)
            transform.FindChild("Pull").localRotation = Quaternion.Euler(45, 0, 0);
        else
            transform.FindChild("Pull").localRotation = Quaternion.Euler(315, 0, 0);
    }


    void OnGUI()
    {
        if (inTrigger)
            EventsLib.DrawInteractivity();
    }
}
