using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class GRL_SoundArea : MonoBehaviour 
{
    private float volume;
    private bool fadingIn = false;
    private bool active = false;
    

    void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        volume = GetComponent<AudioSource>().volume;

        GetComponent<AudioSource>().maxDistance = Mathf.Sqrt(Mathf.Pow(GetComponent<BoxCollider>().size.x / 2, 2) + Mathf.Pow(GetComponent<BoxCollider>().size.z / 2, 2));
        GetComponent<AudioSource>().volume = 0;
    }


    void Update()
    {
        if (active)
        {
            if (!fadingIn)
            {
                if (GetComponent<AudioSource>().volume <= 0)
                {
                    active = false;
                    GetComponent<AudioSource>().Stop();
                }
                else
                    GetComponent<AudioSource>().volume -= 0.2f * Time.deltaTime * volume;
            }
            else
            {
                if (GetComponent<AudioSource>().volume <= volume * GameMusic.musicVolume * GameMusic.masterVolume)
                    GetComponent<AudioSource>().volume += 0.2f * Time.deltaTime * volume;
                else
                    GetComponent<AudioSource>().volume = volume * GameMusic.musicVolume * GameMusic.masterVolume;
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (GetComponent<AudioSource>().clip != null)
            if (other.tag == "Player")
            {
                active = true;
                fadingIn = true;
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
            }
    }


    void OnTriggerExit(Collider other)
    {
        fadingIn = false;
    }
}
