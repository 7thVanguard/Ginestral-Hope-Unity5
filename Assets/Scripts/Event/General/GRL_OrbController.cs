using UnityEngine;
using System.Collections;

public class GRL_OrbController : MonoBehaviour 
{
    World world;
    GameObject greenOrb;

    private float timeCounter;

    private bool active;
    private bool finished;


    void Start()
    {
        greenOrb = GameObject.Instantiate((GameObject)Resources.Load("Particle Systems/Prefabs/Green Orb")) as GameObject;
        greenOrb.transform.parent = this.transform;
        greenOrb.transform.localPosition = Vector3.zero;
    }


    void Update()
    {
        if (!finished)
            if (active)
            {
                finished = (EventsLib.GoAroundPlayer(greenOrb));
                if (finished)
                {
                    transform.GetComponent<AudioSource>().volume = GameMusic.FXVolume;
                    transform.GetComponent<AudioSource>().Play();
                    if (Global.player.currentLife < Global.player.maxLife)
                        Global.player.currentLife++;
                }
            }

        // Reset
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
            if (finished)
            {
                finished = false;
                active = false;
                greenOrb = GameObject.Instantiate((GameObject)Resources.Load("Particle Systems/Prefabs/Green Orb")) as GameObject;
                greenOrb.transform.parent = this.transform;
                greenOrb.transform.localPosition = Vector3.zero;
            }
    }


    void OnTriggerStay(Collider other)
    {
        if (GameFlow.gameMode == GameFlow.GameMode.PLAYER)
            if (other.tag == "Player")
                active = true;
    }
}
