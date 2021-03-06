﻿using UnityEngine;
using System.Collections;

public class EnemyComponent : MonoBehaviour
{
    [HideInInspector] public Color originalColor;
    [HideInInspector] public int maxLife;
    [HideInInspector] public float life;
    [HideInInspector] public bool isSelected;

    private int animCounter;


    void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }


    void Update ()
    {
        // Counter update
        if (animCounter > 0)
        {
            animCounter--;
            if (animCounter == 0)
                gameObject.GetComponent<Renderer>().material.color = originalColor;
        }
    }


    public void Damage(float damage)
    {
        // Damage recieved
        life -= damage;
        DamageAnim();

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void DamageAnim()
    {
        // Changes the color when damaged
        animCounter = 5;

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
