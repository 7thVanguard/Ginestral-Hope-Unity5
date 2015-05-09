using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGGH_NewLevel : MonoBehaviour
{
    public List<GameObject> Spawn = new List<GameObject>();
    public List<GameObject> Despawn = new List<GameObject>();


	void Update ()
    {
        if (GameFlow.readyToReset)
        {
            foreach (GameObject gameObject in Spawn)
                gameObject.SetActive(true);
            foreach (GameObject gameObject in Despawn)
                gameObject.SetActive(true);
        }

        // Despawn when everything has been reset
        if (GameFlow.resetState == GameFlow.ResetState.End && GameFlow.readyToReset)
        {
            foreach (GameObject gameObject in Spawn)
                gameObject.SetActive(true);
            foreach (GameObject gameObject in Despawn)
                gameObject.SetActive(false);

            GameFlow.readyToReset = false;
        }
	}
}
