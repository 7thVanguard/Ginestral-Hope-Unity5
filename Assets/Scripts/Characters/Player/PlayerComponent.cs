using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour 
{
    Player player;

    // Damage relative
    public Color originalColor;
    private int animCounter;


    public void Init(Player player)
    {
        this.player = player;
    }


    void Start()
    {
        // Damage relative
        player.currentLife = player.maxLife;
        // originalColor = gameObject.renderer.material.color;
        animCounter = 0;
    }


    void Update()
    {
        if (Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().volume > GameMusic.masterVolume)
            Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().volume = GameMusic.masterVolume;

        // Hit animation
        //if (animCounter > 0)
        //{
        //    animCounter--;
        //    if (animCounter == 0)
        //        gameObject.renderer.material.color = originalColor;
        //}
    }


    public void Damage(float damage)
    {
        player.currentLife -= damage;

        if (player.currentLife > 0)
        {
            Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Hurt");
            Global.player.animationCoolDown = 30;
        }
        else
        {
            Global.player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().Play("Die");
            Global.player.animationCoolDown = 10000;
        }
    }


    private void DamageAnim()
    {
        animCounter = player.damageAnimTime;

        // gameObject.renderer.material.color = Color.red;
    }
}
