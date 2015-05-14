using UnityEngine;
using System.Collections;

public class GRL_SignalPlatform : MonoBehaviour 
{
    public GameObject emitter;
    public Vector3 activePosition;
    public Vector3 nonActivePosition;
    public float duration;
    public bool toEndPos = false;

    private float interpolation;
    private bool musicPlaying = false;


    void Sttart()
    {
        if (duration < 1)
            duration = 1;
    }


    void Update()
    {
        if (emitter.GetComponent<GRL_PressurePlate>() != null)
            toEndPos = emitter.GetComponent<GRL_PressurePlate>().emitting;
        else if (emitter.GetComponent<GRL_Lever>() != null)
            toEndPos = emitter.GetComponent<GRL_Lever>().emitting;

        if (toEndPos)
            interpolation += Time.deltaTime / duration;
        else
            interpolation -= Time.deltaTime / duration;

        interpolation = Mathf.Clamp(interpolation, 0, 1);
        transform.position = Vector3.Slerp(nonActivePosition, activePosition, interpolation);

        if (Vector3.Distance(transform.position, activePosition) > 0.2f && Vector3.Distance(transform.position, nonActivePosition) > 0.2f)
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


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = transform.FindChild("Collider");
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.transform.parent = null;
    }
}
