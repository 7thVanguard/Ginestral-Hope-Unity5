using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGGH_ThirdInstanceWagon : MonoBehaviour
{
    private Animation animator;

    private bool inTrigger = false;
    private bool finish = false;
	

	void Start() 
	{
		animator = transform.parent.GetComponent<Animation>();
		animator["Anim01"].speed = 0.5f;
	}


    void Update()
    {
        // Reset
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
        {
            animator["Anim01"].speed = -1;
            animator.Play();
            finish = false;
        }
    }


    void OnTriggerStay(Collider other)
    {
        inTrigger = true;

        if (!finish)
            if (other.tag == "Player")
                if (Input.GetKey(KeyCode.E))
                {
                    // Push the MineCart
                    animator["Anim01"].speed = 0.5f;
                    animator.Play();
                    finish = true;
                }
    }


    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }


    void OnGUI()
    {
        if (inTrigger)
            EventsLib.DrawInteractivity();
    }
}
