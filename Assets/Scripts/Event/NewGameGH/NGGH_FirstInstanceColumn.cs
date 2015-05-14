using UnityEngine;
using System.Collections;

public class NGGH_FirstInstanceColumn : MonoBehaviour 
{
    public GameObject column;
    private float timeToSound = 0;
    private bool finish = false;


    void Update()
    {
        // Reset
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
        {
            column.GetComponent<Animation>()["Take 001"].speed = -1;
            column.GetComponent<Animation>().Play();
            finish = false;
        }

        if (timeToSound > 0)
        {
            timeToSound -= Time.deltaTime;
            
            if (timeToSound <= 0)
                column.GetComponent<AudioSource>().Play();
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (!finish)
            if (other.tag == "Player")
            {
                column.GetComponent<Animation>()["Take 001"].speed = 1;
                column.GetComponent<Animation>().Play();
                timeToSound = 0.5f;
                finish = true;
            }
    }
}
