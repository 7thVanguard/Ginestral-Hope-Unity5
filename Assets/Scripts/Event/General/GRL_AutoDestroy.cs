using UnityEngine;
using System.Collections;

public class GRL_AutoDestroy : MonoBehaviour 
{
    public float time;

	void Update ()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(this.gameObject);
	}
}
