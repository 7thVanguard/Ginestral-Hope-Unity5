using UnityEngine;
using System.Collections;

public class GRL_FireEmitter : MonoBehaviour
{
    // Changed from the impact of the fire ball
    public bool emitting;


    void OnTriggerStay(Collider other)
    {
        if (emitting)
            if (other.CompareTag("Player"))
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    if (Global.player.fireCharges < 3)
                        Global.player.fireCharges++;
                    emitting = false;
                }
    }
}
