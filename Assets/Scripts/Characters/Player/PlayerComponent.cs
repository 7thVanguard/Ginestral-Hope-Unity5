using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour 
{
	Player player;
	
	// Damage relative
	public Color originalColor;
	private int animCounter;
	
	private MovieTexture movie;
	private float movieTime;
	private float movieDelay;
	private bool playerDead;
	private bool moviePlaying;
	
	
	public void Init(Player player)
	{
		this.player = player;
		movie = (MovieTexture)Resources.Load("Cinematics/GameOver");
		movieTime = movie.duration;
		movieDelay = 2;
		playerDead = false;
	}
	
	
	void Start()
	{
		// Damage relative
		//player.currentLife = player.maxLife;
		// originalColor = gameObject.renderer.material.color;
		animCounter = 0;
	}
	
	
	void Update()
	{
		if (Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().volume > GameMusic.masterVolume)
			Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().volume = GameMusic.masterVolume;
		
		if (playerDead)
		{
			movieDelay -= Time.deltaTime;
			
			if (movieDelay <= 0)
			{
				if (!movie.isPlaying)
				{
					movie.Play();
					movieTime = movie.duration;
					Global.player.playerObj.transform.FindChild("MovieMusic").GetComponent<AudioSource>().Play();
					GameFlow.onCameraTravel = true;
				}
				
				movieTime -= Time.deltaTime;
				
				if (Input.GetKey(KeyCode.Space))
					movieTime = 0;
				
				if (movieTime <= 0.5f)
					movieTime = 0;
				
				if (movieTime <= 0.1f)
				{
					movie.Stop();
					Global.player.playerObj.transform.FindChild("FXPlayer").GetComponent<AudioSource>().Stop();
					Global.player.currentLife = 1;
					playerDead = false;
					GUIGHMainMenu.reset = true;
					GUIGHMainMenu.blackSpace.SetActive(true);
					GUIGHMainMenu.alphaCounterBlackScreen = 1;
					movieDelay = 2;
					GameFlow.onCameraTravel = false;
				}
			}
		}
	}
	
	
	void OnGUI()
	{
		if (movie.isPlaying)
		{
			GUI.color = Color.white;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie);
			
			GUIStyle style = new GUIStyle();
			style.font = (Font)Resources.Load("UI/Fonts/Amigo-Regular");
			style.fontSize = 30;
			style.normal.textColor = Color.white;
			GUI.Label(new Rect(Screen.width * 6.2f / 8, Screen.height * 7.6f / 8, Screen.width * 2 / 8, Screen.height * 0.5f / 8), "Press Spacebar to skip", style);
		}
	}
	
	
	public void Damage(float damage)
	{
		if (player.currentLife > 0)
		{
			playerDead = false;
			
			player.currentLife -= damage;
			
			if (player.currentLife > 0)
			{
				Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Hurt");
				Global.player.animationCoolDown = 30;
			}
			else if (player.currentLife <= 0)
			{
				Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Die");
				Global.player.animationCoolDown = 300;
				playerDead = true;
			}
		}
	}
	
	
	private void DamageAnim()
	{
		animCounter = player.damageAnimTime;
		
		// gameObject.renderer.material.color = Color.red;
	}
}


