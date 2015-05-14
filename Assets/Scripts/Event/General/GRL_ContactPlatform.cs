using UnityEngine;
using System.Collections;

public class GRL_ContactPlatform : MonoBehaviour 
{
    public Vector3 initialPosition;
    public Vector3 endPosition;
    public float duration;

    private float interpolation = 0;
    private bool onStay = false;


    void Sttart()
    {
        if (duration < 1)
            duration = 1;
    }


    void Update()
    {
        if (!GameFlow.pause)
        {
            if (onStay)
                interpolation += Time.deltaTime / duration;
            else
                interpolation -= Time.deltaTime / duration;

            interpolation = Mathf.Clamp(interpolation, 0, 1);
            transform.position = Vector3.Slerp(initialPosition, endPosition, interpolation);

            if (Vector3.Distance(transform.position, endPosition) > 0.2f && Vector3.Distance(transform.position, initialPosition) > 0.2f)
            {
                if (!transform.GetComponent<AudioSource>().isPlaying)
                    transform.GetComponent<AudioSource>().Play();

                if (transform.GetComponent<AudioSource>().volume < 1)
                    transform.GetComponent<AudioSource>().volume += Time.deltaTime;
            }
            else
            {
                if (transform.GetComponent<AudioSource>().volume > 0)
                    transform.GetComponent<AudioSource>().volume -= Time.deltaTime;

                if (transform.GetComponent<AudioSource>().volume == 0)
                    transform.GetComponent<AudioSource>().Stop();
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            onStay = true;
            Global.player.playerObj.transform.parent = transform;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onStay = false;
            Global.player.playerObj.transform.parent = null;
        }
    }
}
