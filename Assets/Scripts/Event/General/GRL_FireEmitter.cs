using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GRL_FireEmitter : MonoBehaviour
{
    [HideInInspector] public bool startEmitting;
    // Changed from the impact of the fire ball
    public bool emitting;
    
    private AudioSource audio;

    void Start()
    {
        startEmitting = emitting;
        audio = transform.GetComponent<AudioSource>();
    }


    void Update()
    {
        if (emitting)
        {
            transform.FindChild("Fire").gameObject.SetActive(true);
            if (!audio.isPlaying)
                audio.Play();
        }
        else
        {
            transform.FindChild("Fire").gameObject.SetActive(false);
            if (audio.isPlaying)
                audio.Stop();
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (emitting)
        {
            if (other.CompareTag("Player"))
				if (Input.GetKeyUp(KeyCode.Mouse1) || GameManager.padState.Buttons.X == ButtonState.Pressed)
                {
                    if (Global.player.fireCharges < 3)
                        Global.player.fireCharges++;
                    emitting = false;
                }
        }
        else
        {
            if (other.CompareTag("Fire"))
            {
                emitting = true;
                other.tag = "Untagged";
            }
        }
    }
}
