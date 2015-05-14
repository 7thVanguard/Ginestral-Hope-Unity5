using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireThrower : MonoBehaviour 
{
    private float objectiveTime = 0;
    private float timeCounter = 1000;
    private int listPosition = 0;

    public List<float> List = new List<float>();

    public float beginDelay;
    public int damage;


	void Update ()
    {
        // We wait for the beginning
        if (beginDelay > 0)
            beginDelay -= Time.deltaTime / 3;
        // We make sure the list is not empty
        else if (List.Count > 0)
        {
            timeCounter += Time.deltaTime / 3;

            if (timeCounter >= objectiveTime)
            {
                // Counter logic with steps
                listPosition++;
                if (listPosition >= List.Count)
                    listPosition = 0;

                objectiveTime = List[listPosition];
                timeCounter = 0;

                // Launch fireball
                GameObject fireBall = (GameObject)Resources.Load("Particle Systems/Prefabs/Fire Ball");
                fireBall = GameObject.Instantiate(fireBall) as GameObject;
                fireBall.name = "Fire Ball";
                fireBall.transform.position = transform.position + transform.up;

                fireBall.AddComponent<FireBallBehaviour>();
                fireBall.GetComponent<FireBallBehaviour>().direction = transform.up;
                fireBall.GetComponent<FireBallBehaviour>().damage = damage;

                transform.GetComponent<AudioSource>().volume = GameMusic.FXVolume;
                transform.GetComponent<AudioSource>().Play();
            }
        }
	}
}
