using UnityEngine;
using System.Collections;

public class C_EndGame : MonoBehaviour
{
    private MovieTexture movie;
    private AudioSource movieSource;

    private float movieTime;
    private bool triggered;



    void Start()
    {
        movie = (MovieTexture)Resources.Load("Cinematics/TheEnd");
        movieSource = transform.GetComponent<AudioSource>();

        movieTime = movie.duration;
        triggered = false;
    }


    void OnTriggerEnter()
    {
        GameFlow.onCameraTravel = true;
        triggered = true;
        movie.Play();
        movieSource.Play();
    }


    void Update()
    {
        if (triggered)
        {
            movieTime -= Time.deltaTime;
            if (movieTime <= 0)
                movie.Stop();
        }
    }


    void OnGUI()
    {
        if (triggered)
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
}
