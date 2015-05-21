using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRL_FireDespawner : MonoBehaviour 
{
    public GameObject door;

    public List<GameObject> List = new List<GameObject>();


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.GetComponent<GRL_FireDoor>().totalClose = true;
            StartCoroutine(Despawn());
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        foreach (GameObject go in List)
            go.SetActive(false);
    }
}
