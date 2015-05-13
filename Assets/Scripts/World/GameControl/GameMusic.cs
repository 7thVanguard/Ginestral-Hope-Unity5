using UnityEngine;
using System.Collections;

public class GameMusic
{
    private static AudioSource playerAudio;

    public static float masterVolume = 1;
    public static float musicVolume = 1;
    public static float ambientVolume = 1;
    public static float FXVolume = 1;

    public static bool playing = false;
    public static bool fadingOut = false;
    public static bool fadingIn = true;


    public static void Init()
    {
        playerAudio = Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>();
        playerAudio.volume = 0;
        playerAudio.Play();
    }


    public static void Update()
    {
        if (fadingOut)
        {
            playerAudio.volume = Mathf.Lerp(playerAudio.volume, 0, 0.03f);
        }
        else if (fadingIn)
        {
            playerAudio.volume = Mathf.Lerp(playerAudio.volume, 1, 0.03f);
        }
    }
}