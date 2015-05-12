using UnityEngine;
using System.Collections;

public class GRL_Codex : MonoBehaviour
{
    public int tableNumber;

    private bool active = false;
    private bool inTrigger = false;
	
	void Update () 
    {
	
	}


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;

            if (Input.GetKey(KeyCode.E))
            {
                active = true;
                GameGUI.codexMode.GetComponent<GUICodexMode>().ActivateTable(tableNumber);
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inTrigger = false;
    }


    void OnGUI()
    {
        if (inTrigger)
            if (!active)
                EventsLib.DrawInteractivity();
    }
}
