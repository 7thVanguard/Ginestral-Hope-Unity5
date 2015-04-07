using UnityEngine;
using System.Collections;

public class Player
{
    public GameObject playerObj;
    public CharacterController controller;
    public GameObject lookAtObjective;

    // Statistics
    public float maxLife = 3;
    public float currentLife = 3;
    public int orbsCollected = 0;

    // Movement
    public float godModeSpeed = 25;
    public float runSpeed = 6.5f;
    public float walkSpeed = 4;

    public float acceleration = 0.5f;

    public float jumpInitialSpeed = 8;
    public float jumpGravityMultiplier = 2.5f;

    public bool isMoving;

    // Detection
    public int constructionDetection = 5;

    // Animation
    public int damageAnimTime = 5;

    // Skills
    public bool unlockedSkillFireBall = false;




    public Player(World world)
    {
        Init(world);
    }


    public void Init(World world)
    {
        //+ Player creation
        playerObj = GameObject.FindGameObjectWithTag("Player");

        // Head atributes
        playerObj.name = "Player";
        playerObj.tag = "Player";
        playerObj.layer = LayerMask.NameToLayer("Player");

        // Set transforms
        playerObj.transform.position = new Vector3(4, 35, 4);
        playerObj.transform.eulerAngles = Vector3.zero;

        // Player components creation
        controller = playerObj.GetComponent<CharacterController>();
        controller.slopeLimit = 46;

        //+ Player initializations
        playerObj.GetComponent<PlayerComponent>().Init(this);

        lookAtObjective = new GameObject();
        lookAtObjective.name = "Player Look At Objective";
        lookAtObjective.transform.parent = playerObj.transform;
        lookAtObjective.transform.localPosition = Vector3.zero;
        lookAtObjective.transform.localEulerAngles = Vector3.zero;
        lookAtObjective.transform.localScale = Vector3.zero;
    }
}
