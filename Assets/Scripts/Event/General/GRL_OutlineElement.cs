using UnityEngine;
using System.Collections;

public class GRL_OutlineElement : MonoBehaviour {

	private float outlineValue = 0.008f;
	public Color color;

	private GameObject outline;

	void Awake()
	{
		outline = EventsLib.DrawOutlineShader(this.transform.gameObject, outline, outlineValue, color);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
