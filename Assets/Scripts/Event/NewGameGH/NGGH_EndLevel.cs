using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public enum AnimState { MoveDown, MoveUp }
public class NGGH_EndLevel : MonoBehaviour 
{
	private AnimState animState = AnimState.MoveUp;
	
	private MovieTexture movie;
	private AudioSource movieSource;
	
	private Texture2D sevenV;
	private Texture2D blackSpace;
	
	private float alphaCounterBlackScreen = 0;
	private bool active = false;
	private bool inTrigger = false;
	
	//Fire Gem Transform
	public Transform fireGemTransform;
	private Vector3 fireGemInitialPosition;
	private Transform fireGemAuxTransform;
	
	public float gemRotationSpeed;
	
	public float frameToframeAnimSpeed;
	public float animTime;
	private float animTimeInit;
	public float animSlerpTime;
	
	private float timeCounter;
	private bool launchMovie;
	
	
	void Start()
	{
		movie = (MovieTexture)Resources.Load("Cinematics/Cinematica02_English");
		movieSource = transform.GetComponent<AudioSource>();
		
		blackSpace = (Texture2D)Resources.Load("UI/BlackScreen");
		fireGemInitialPosition = fireGemTransform.position;
		
		fireGemAuxTransform = fireGemTransform;
		
		animTimeInit = animTime;
		
		timeCounter = 0;
		launchMovie = false;
	}
	
	
	void Update()
	{
		if (active)
		{
			if (alphaCounterBlackScreen < 255)
				alphaCounterBlackScreen += 250 * Time.deltaTime;
			
			if (alphaCounterBlackScreen > 255)
				alphaCounterBlackScreen = 255;
		}
		
		if (Input.GetKey(KeyCode.Space))
			timeCounter = movie.duration;
		
		AnimateFireGem();
	}
	
	
	void OnGUI()
	{
		if (active)
		{
			GUI.color = new Color32(0, 0, 0, (byte)alphaCounterBlackScreen);
			GUI.DrawTextureWithTexCoords(new Rect(0, 0, Screen.width, Screen.height), blackSpace, new Rect(0, 0, 1, 1));
			
			if (alphaCounterBlackScreen > 250)
			{
				if (!launchMovie)
				{
					movie.Play();
					GameFlow.onCameraTravel = true;
					movieSource.Play();
					
					timeCounter = movie.duration * 2;
					launchMovie = true;
				}
				timeCounter -= Time.deltaTime;
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie);
				
				GUIStyle style = new GUIStyle();
				style.font = (Font)Resources.Load("UI/Fonts/Amigo-Regular");
				style.fontSize = 30;
				style.normal.textColor = Color.white;
				GUI.Label(new Rect(Screen.width * 6.2f / 8, Screen.height * 7.6f / 8, Screen.width * 2 / 8, Screen.height * 0.5f / 8), "Press Spacebar to skip", style);
				
				if (timeCounter <= 0)
				{
					movie.Stop();
					GameFlow.onCameraTravel = false;
					movieSource.Stop();
					Application.LoadLevel(3);
					
					GUIGHMainMenu.newGame = true;
					GUIGHMainMenu.fadingIn = true;
					
					GUIGHMainMenu.blackSpace.SetActive(true);
					Global.player.immune = true;
				}
			}
		}
		else
		{
			if (GameFlow.gameState == GameFlow.GameState.GAME)
				if (!GameFlow.pause)
					if (inTrigger)
						EventsLib.DrawInteractivity();
		}
	}
	
	
	void OnTriggerStay (Collider other)
	{
		inTrigger = true;
		
		if (Input.GetKeyDown(KeyCode.E))
			if (other.tag == "Player")
				active = true;
	}
	
	
	void OnTriggerExit(Collider other)
	{
		inTrigger = false;
	}
	
	private void AnimateFireGem()
	{
		fireGemTransform.Rotate(new Vector3(0, Time.deltaTime * gemRotationSpeed, 0));
		fireGemTransform.position = Vector3.Slerp(fireGemTransform.position, fireGemAuxTransform.position, animSlerpTime);
		
		switch (animState)
		{
		case AnimState.MoveUp:
			UpBehaviour();
			break;
			
		case AnimState.MoveDown:
			DownBehaviour();
			break;
			
		default:
			break;
		}
	}
	
	
	#region Animate Gem Auxiliar Functions
	
	//Set
	private void SetUp()
	{
		animTime = animTimeInit;
		animState = AnimState.MoveUp;	
	}
	
	private void SetDown()
	{
		animTime = animTimeInit;
		animState = AnimState.MoveDown;
	}
	
	//Behaviours
	private void UpBehaviour()
	{
		animTime -= Time.deltaTime;
		
		if (animTime < 0)
			SetDown();
		
		fireGemAuxTransform.Translate(new Vector3(0, Time.deltaTime * frameToframeAnimSpeed, 0));
		
	}
	
	private void DownBehaviour()
	{
		animTime -= Time.deltaTime;
		if (animTime < 0) 
			SetUp();
		
		fireGemAuxTransform.Translate(new Vector3(0, Time.deltaTime * -frameToframeAnimSpeed, 0));
		
	}
	
	#endregion
}
