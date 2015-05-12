using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRL_FadingInteractivity : MonoBehaviour 
{
    public List<Material> List = new List<Material>();
	
	// Update is called once per frame
	void Update () 
    {
        foreach (Material material in List)
            EventsLib.FadeMaterialEmission(material);
	}
}
