using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class GRL_FireStatue : MonoBehaviour
{
	public GameObject door;
	public GameObject firstBrazier;
	public GameObject secondBrazier;
	public GameObject thirdBrazier;
	
	public List<GameObject> List = new List<GameObject>();
	
	
	void Start()
	{
		//this.transform.FindChild("Fire Orb").gameObject.SetActive(false);
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		this.transform.FindChild("Fire Orb").gameObject.SetActive(true);
	}
	
	
	void OnTriggerStay(Collider other)
	{
		
		if (Input.GetKeyUp(KeyCode.Mouse1) || GameManager.padState.Buttons.X == ButtonState.Pressed)
			if (other.CompareTag("Player"))
		{
			door.GetComponent<GRL_FireDoor>().close = true;
			firstBrazier.GetComponent<GRL_FireEmitter>().emitting = firstBrazier.GetComponent<GRL_FireEmitter>().startEmitting;
			secondBrazier.GetComponent<GRL_FireEmitter>().emitting = secondBrazier.GetComponent<GRL_FireEmitter>().startEmitting;
			thirdBrazier.GetComponent<GRL_FireEmitter>().emitting = thirdBrazier.GetComponent<GRL_FireEmitter>().startEmitting;
			
			foreach (GameObject torch in List)
				torch.GetComponent<GRL_FireEmitter>().emitting = torch.GetComponent<GRL_FireEmitter>().startEmitting;
			
			Global.player.fireCharges = 3;
			
			EventsLib.SpawnParticlesOnPlayer("Particle Systems/Prefabs/Absorb Energy");
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		this.transform.FindChild("Fire Orb").gameObject.SetActive(false);
	}
}
