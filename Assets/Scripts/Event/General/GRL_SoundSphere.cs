using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class GRL_SoundSphere : MonoBehaviour
{
    private float volume;

    void Awake()
    {
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = GetComponent<AudioSource>().maxDistance + 1;

        volume = GetComponent<AudioSource>().volume;
    }


    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<AudioSource>().clip != null)
            if (other.tag == "Player")
                GetComponent<AudioSource>().Play();
    }


    void OnTriggerStay(Collider other)
    {
        if (GetComponent<AudioSource>().clip != null)
            if (other.tag == "Player")
            {
                GetComponent<AudioSource>().volume = (1 - Vector3.Distance(Global.player.playerObj.transform.position, transform.position) / GetComponent<AudioSource>().maxDistance) * volume * GameMusic.ambientVolume * GameMusic.masterVolume;
            }
    }


    void OnTriggerExit(Collider other)
    {
        if (GetComponent<AudioSource>().clip != null)
            if (other.tag == "Player")
                GetComponent<AudioSource>().Stop();
    }
}
