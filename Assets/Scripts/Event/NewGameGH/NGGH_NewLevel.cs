﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGGH_NewLevel : MonoBehaviour
{
    public List<GameObject> Spawn = new List<GameObject>();
    public List<GameObject> Despawn = new List<GameObject>();


	void Update ()
    {
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
        {
            Global.player.playerObj.transform.position = new Vector3(34.5f, 1, 17);
            Global.player.playerObj.transform.eulerAngles = new Vector3(0, 160, 0);
        }

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
