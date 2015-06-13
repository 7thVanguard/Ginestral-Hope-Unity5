using UnityEngine;
using System.Collections;

public class GRL_WaterCannon : MonoBehaviour
{
    public GameObject emiterObject;
    public GameObject fireObject;

    public float timeToExtinguish;
    private float timeCounter;

    [HideInInspector] public bool startEmitting;


    void Update()
    {
        if (emiterObject.GetComponent<GRL_Lever>().emitting)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeToExtinguish)
                fireObject.GetComponent<GRL_FireEmitter>().emitting = false;

            transform.FindChild("Particles").gameObject.SetActive(true);
        }
        else
        {
            timeCounter = 0;
            transform.FindChild("Particles").gameObject.SetActive(false);
        }
    }
}
