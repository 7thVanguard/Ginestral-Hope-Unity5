﻿using UnityEngine;
using System.Collections;

public static class EventsLib
{
	//+ Ambient
	private static Color objectiveColor;
	public static void SetRenderambient(Color color) 
	{ 
		objectiveColor = color; 
	}
	public static void UpdateRenderambient()
	{
		RenderSettings.ambientLight = RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, objectiveColor, 0.005f);
	}
	
	
	//+ Doors
	private static GameObject door1;
	private static GameObject door2;
	private static Vector3 objectivePosition1;
	private static Vector3 objectivePosition2;
	public static void SetDoorOpenDoubleSlider(GameObject firstDoor, Vector3 firstDoorObjectivePosition, GameObject secondDoor, Vector3 secondDoorObjectivePosition)
	{
		door1 = firstDoor;
		door2 = secondDoor;
		objectivePosition1 = firstDoorObjectivePosition;
		objectivePosition2 = secondDoorObjectivePosition;
	}
	public static void UpdateDoorOpenDoubleSlider()
	{
		door1.transform.localPosition = Vector3.Slerp(door1.transform.localPosition, objectivePosition1, 0.02f);
		door2.transform.localPosition = Vector3.Slerp(door2.transform.localPosition, objectivePosition2, 0.02f);
	}
	
	
	//+ Efects
	public static bool GoAroundPlayer(GameObject obj)
	{
		float distance = Vector3.Distance(obj.transform.position, obj.transform.parent.position + new Vector3(0, 0.5f, 0));
		
		// Set player as parent of the Orb
		obj.transform.parent = Global.player.playerObj.transform;
		
		// Rotate around player, incrementing speed while getting near
		obj.transform.RotateAround(obj.transform.parent.position, Vector3.up,
		                           (200 + (3 - distance) * 175) * Time.deltaTime);
		
		
		// Lerp towards player
		obj.transform.position = Vector3.Lerp(obj.transform.position, 
		                                      new Vector3(obj.transform.parent.position.x, obj.transform.parent.position.y + 0.5f, obj.transform.position.z),
		                                      0.02f + (3 - distance) * 0.012f);
		
		// Change size depending on the distance to the player
		if (distance < 3)
		{
			obj.transform.FindChild("Ball").GetComponent<ParticleSystem>().startSize =
				Mathf.Lerp(obj.transform.FindChild("Ball").GetComponent<ParticleSystem>().startSize,
				           obj.transform.FindChild("Ball").GetComponent<ParticleSystem>().startSize * (distance / 3),
				           0.3f);
			
			obj.transform.FindChild("Trail").GetComponent<ParticleSystem>().startSize =
				Mathf.Lerp(obj.transform.FindChild("Trail").GetComponent<ParticleSystem>().startSize,
				           obj.transform.FindChild("Trail").GetComponent<ParticleSystem>().startSize * (distance / 3),
				           0.3f);
			
			if (obj.transform.FindChild("Ball").GetComponent<ParticleSystem>().startSize < 0.35f)
				obj.transform.FindChild("Glow").localScale = Vector3.zero;
		}
		
		if (Vector3.Distance(obj.transform.position, obj.transform.parent.position + new Vector3(0, 0.5f, 0)) < 0.1f)
		{
			GameObject.Destroy(obj);
			Global.player.orbsCollected++;
			GameFlow.orbCollected = true;
			return true;
		}
		else
			return false;
	}
	public static float FadeIn(float framesCounter, float framesCounterOnStay)
	{
		if (framesCounter <= framesCounterOnStay)
			framesCounter += 2 * Time.deltaTime;
		else
			framesCounter = framesCounterOnStay;
		
		return framesCounter;
	}
	public static float FadeOut(float framesCounter)
	{
		if (framesCounter > 0)
			framesCounter -= Time.deltaTime;
		
		return framesCounter;
	}
	public static void DrawTutorial(Texture2D tutorial, float framesCounter)
	{
		if (framesCounter > 0 && !GameFlow.pause && GameFlow.gameState == GameFlow.GameState.GAME)
		{
			GUI.color = new Color(1, 1, 1, framesCounter);
			GUI.DrawTextureWithTexCoords(new Rect(6 * Screen.width / 10, Screen.height / 6, 2.5f * Screen.width / 10, (2.5f * Screen.width / 10) * 1.4f),
			                             tutorial, new Rect(0, 0, 1, 1));
			GUI.color = new Color(1, 1, 1, 1);
		}
	}
	public static GameObject DrawOutlineShader(GameObject parentObj, GameObject outline, float outlineValue, Color color)
	{
		outline = new GameObject("Outline");
		outline.transform.parent = parentObj.transform;
		
		outline.transform.localPosition = Vector3.zero;
		outline.transform.localEulerAngles = Vector3.zero;
		outline.transform.localScale = Vector3.one;
		
		outline.AddComponent<MeshFilter>();
		outline.AddComponent<MeshRenderer>();
		outline.GetComponent<MeshFilter>().mesh = parentObj.transform.GetComponent<MeshFilter>().mesh;
		
		outline.GetComponent<Renderer>().material.shader = Shader.Find("Ginestral/Silhouette Only");
		outline.GetComponent<Renderer>().material.SetColor("_OutlineColor", color);
		outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineValue);
		
		return outline;
	}
	public static void FadeMaterialEmission(Material material)
	{
		float emissionValue = Mathf.PingPong(Time.time / 20, 0.05f);
		
		Color baseColor = Color.white;
		Color finalColor = baseColor * Mathf.LinearToGammaSpace(emissionValue);
		
		material.SetColor("_EmissionColor", finalColor);
	}
	public static void DrawInteractivity()
	{
		GUI.DrawTextureWithTexCoords(new Rect(Screen.width * 2 / 3, Screen.height / 2 - 30, 60, 60), (Texture2D)Resources.Load("UI/Interactivity"),
		                             new Rect(0, 0, 1, 1));
	}
	
	public static void SpawnParticlesOnPlayer(string prefabPath)
	{
		GameObject particle = GameObject.Instantiate((GameObject)Resources.Load(prefabPath));
		
		particle.transform.parent = Global.player.playerObj.transform.FindChild("Mesh").FindChild("Bip001").transform;
		particle.transform.localPosition = Vector3.zero;
		particle.layer = LayerMask.NameToLayer("Ignore Raycast");
		
		GameObject.Destroy(particle, 7);
	}
	
	public static void SpawnParticlesOnObject(GameObject target, float yPositionOffset, string prefabPath)
	{
		GameObject particle = GameObject.Instantiate((GameObject)Resources.Load(prefabPath));
		
		particle.transform.parent = target.transform;
		particle.transform.localPosition = new Vector3(0, yPositionOffset, 0);
		
		GameObject.Destroy(particle, 7);
	}
}


