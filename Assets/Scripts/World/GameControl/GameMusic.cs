using UnityEngine;
using System.Collections;

public class GameMusic
{
    private static AudioSource playerAudio;

    public static float maxVolume = 1;

    public static bool playing = false;
    public static bool fadingOut = false;
    public static bool fadingIn = true;


    public static void Init()
    {
        playerAudio = Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>();
    }


    public static void Update()
    {
        if (fadingOut)
        {
            FadeOut(Global.player.playerObj.transform.FindChild("MusicPlayer"));
        }
        else if (fadingIn)
        {
            FadeIn(Global.player.playerObj.transform.FindChild("MusicPlayer"));
        }
        else if (!playing)
        {
            if (GameFlow.gameState == GameFlow.GameState.MENU)
            {
                playerAudio.Play();
                playing = true;
            }
        }
    }


    public static bool CheckFadeOut(Transform audioTransform)
    {
        if (audioTransform.GetComponent<AudioSource>().volume <= 0)
            return true;
        else
        {
            FadeOut(audioTransform);
            return false;
        }
    }

    private static void FadeIn(Transform audioTransform)
    {
        if (audioTransform.GetComponent<AudioSource>().volume < 1)
            audioTransform.GetComponent<AudioSource>().volume += 0.2f * Time.deltaTime;

        if (audioTransform.GetComponent<AudioSource>().volume >= 1)
            fadingIn = false;
    }


    private static void FadeOut(Transform audioTransform)
    {
        if (audioTransform.GetComponent<AudioSource>().volume > 0)
            audioTransform.GetComponent<AudioSource>().volume -= 0.5f * Time.deltaTime;

        if (audioTransform.GetComponent<AudioSource>().volume <= 0)
            fadingOut = false;
    }


    public static void SelectClips(string saveName)
    {
        switch (saveName)
        {
            case "NewGameGH":
                {
                    Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/OST/Gameplay/Cavern of Time");
                    Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().Play();
                }
                break;
            case "CaverninaFirstLevel":
                {
                    Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/OST/Gameplay/The Land of Lament V2");
                    Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().Play();
                }
                break;
            case "CaverninaBoss":
                {
                    Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/OST/Boss Stage/Rise of The Titans V2");
                    Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().Play();
                }
                break;
            default:
                break;
        }
    }
}