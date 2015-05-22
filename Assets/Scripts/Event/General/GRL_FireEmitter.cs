using UnityEngine;
using System.Collections;

public class GRL_FireEmitter : MonoBehaviour
{
    [HideInInspector] public bool startEmitting;
    // Changed from the impact of the fire ball
    public bool emitting;
    

    void Start()
    {
        startEmitting = emitting;
    }


    void Update()
    {
        if (emitting)
            transform.FindChild("Fire").gameObject.SetActive(true);
        else
            transform.FindChild("Fire").gameObject.SetActive(false);
    }


    void OnTriggerStay(Collider other)
    {
        if (emitting)
        {
            if (other.CompareTag("Player"))
                if (Input.GetKeyUp(KeyCode.Mouse1))
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
