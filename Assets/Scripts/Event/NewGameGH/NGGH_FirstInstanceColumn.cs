using UnityEngine;
using System.Collections;

public class NGGH_FirstInstanceColumn : MonoBehaviour 
{
    public GameObject column;
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
    }


    void OnTriggerStay(Collider other)
    {
        if (!finish)
            if (other.tag == "Player")
            {
                column.GetComponent<Animation>()["Take 001"].speed = 1;
                column.GetComponent<Animation>().Play();
                finish = true;
            }
    }
}
