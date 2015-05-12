using UnityEngine;
using System.Collections;

public class GRL_Codex : MonoBehaviour
{
    public int tableNumber;
    private bool inTrigger = false;
	
	void Update () 
    {
	
	}


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;

            if (!GameFlow.onInterface)
                if (Input.GetKey(KeyCode.E))
                    GameGUI.codexMode.GetComponent<GUICodexMode>().ActivateTable(tableNumber);
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
            if (!GameFlow.onInterface)
                EventsLib.DrawInteractivity();
    }
}
