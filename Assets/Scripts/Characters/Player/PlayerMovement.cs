using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class PlayerMovement
{
    private World world;
    private Player player;
    private MainCamera mainCamera;

    //private GamePadState padState;
    //private GamePadState previousPadState;
    //private PlayerIndex padIndex;
    
    private Vector3 objectiveDirection;
    private Vector3 interpolateDirection;

    private int jumpAnimationCounter = 0;


    public PlayerMovement(World world, Player player, MainCamera mainCamera)
    {
        this.world = world;
        this.player = player;
        this.mainCamera = mainCamera;
    }


    public void NormalMovementUpdate()
    {
        //padState = GamePad.GetState(padIndex);

        // Auto damage
        if (Input.GetKeyUp(KeyCode.B))
            player.playerObj.GetComponent<PlayerComponent>().Damage(1);

        // Calculates the module of the speed
        float root = Mathf.Sqrt(player.runSpeed * player.runSpeed / 2);

        // Acceleration and deceleration
        interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, player.acceleration),
                                            objectiveDirection.y,
                                            Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, player.acceleration));

        // Calculates the direction
        HorizontalMovement(player.runSpeed, root);

        // Jump
        if (player.controller.isGrounded)
        {
            if ((Input.GetKey(KeyCode.Space))/* || padState.Buttons.A == ButtonState.Pressed*/)
            {
                objectiveDirection = new Vector3(objectiveDirection.x, player.jumpInitialSpeed, objectiveDirection.z);
            }
            else
                objectiveDirection.y = 0;

            // Set back the counter to 0 preventing the jump animation play for little air times
            jumpAnimationCounter = 0;
        }
        else
        {
            objectiveDirection += new Vector3(0, -GamePhysics.gravity * player.jumpGravityMultiplier, 0) * Time.deltaTime;

            // Jump animation activation
            jumpAnimationCounter++;
            if (jumpAnimationCounter >= 3)
                Animation("Jump", false, true); 
        }

        // Assign movement
        player.controller.Move(interpolateDirection * Time.deltaTime);

        // Animation speed Control
        player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>()["Run"].speed = 1.1f;
    }


    public void DeveloperMovementUpdate()
    {
        // Calculates the module of the speed
        float root = Mathf.Sqrt(player.godModeSpeed * player.godModeSpeed / 2);

        // Acceleration and deceleration
        interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, player.acceleration),
                                            objectiveDirection.y,
                                            Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, player.acceleration));
        
        // Calculates the direction
        HorizontalMovement(player.godModeSpeed, root);

        // Fly
        if (Input.GetKey(KeyCode.E))
            objectiveDirection = new Vector3(objectiveDirection.x, player.godModeSpeed, objectiveDirection.z);
        else if (Input.GetKey(KeyCode.Q))
            objectiveDirection = new Vector3(objectiveDirection.x, -player.godModeSpeed, objectiveDirection.z);
        else
            objectiveDirection = new Vector3(objectiveDirection.x, 0, objectiveDirection.z);

        // Assign movement
        player.controller.Move(interpolateDirection * Time.deltaTime);
    }


    private void HorizontalMovement(float speed, float root)
    {
        player.isMoving = true;

        // Keyboard
        // Assign a direction depending on the input introduced
        if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A)))
            objectiveDirection = new Vector3(-root, objectiveDirection.y, root);
        else if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D)))
            objectiveDirection = new Vector3(root, objectiveDirection.y, root);
        else if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.A)))
            objectiveDirection = new Vector3(-root, objectiveDirection.y, -root);
        else if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.D)))
            objectiveDirection = new Vector3(root, objectiveDirection.y, -root);
        else
        {
            if (Input.GetKey(KeyCode.D))
                objectiveDirection = new Vector3(speed, objectiveDirection.y, 0);
            else if (Input.GetKey(KeyCode.A))
                objectiveDirection = new Vector3(-speed, objectiveDirection.y, 0);
            else if (Input.GetKey(KeyCode.W))
                objectiveDirection = new Vector3(0, objectiveDirection.y, speed);
            else if (Input.GetKey(KeyCode.S))
                objectiveDirection = new Vector3(0, objectiveDirection.y, -speed);
            else
            {
                objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
                player.isMoving = false;
            }
        }

        // GamePad
        //if ((padState.ThumbSticks.Left.X != 0) || (padState.ThumbSticks.Left.Y != 0))
        //    objectiveDirection = new Vector3(padState.ThumbSticks.Left.X * speed, objectiveDirection.y, padState.ThumbSticks.Left.Y * speed);

        // Player looking at movement direction
        if (player.isMoving)
        {
            // Set the objective direction depending on the camera rotation
            player.playerObj.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            objectiveDirection = player.playerObj.transform.TransformDirection(objectiveDirection);

            // objective
            player.lookAtObjective.transform.LookAt(new Vector3(objectiveDirection.x + player.playerObj.transform.position.x,
                                                            player.playerObj.transform.position.y,
                                                            objectiveDirection.z + player.playerObj.transform.position.z));
            // Lerp to objective
            player.playerObj.transform.FindChild("Mesh").rotation = Quaternion.Lerp(player.playerObj.transform.FindChild("Mesh").rotation,
                                                                    player.lookAtObjective.transform.rotation, 0.25f);
        }

        // Animation
        if (player.isMoving)
            Animation("Run", true, false); 
        else
            Animation("Idle", true, false); 
    }


    private void Animation(string animationName, bool grounded, bool air)
    {
        if (player.currentLife > 0)
        {
            if (player.animationCoolDown == 0)
            {
                // Basic animation
                if (grounded)
                {
                    if (player.controller.isGrounded)
                        player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().CrossFade(animationName);
                }
                else if (air)
                {
                    if (!player.controller.isGrounded)
                        player.playerObj.transform.FindChild("Mesh").GetComponent<Animation>().CrossFade(animationName);
                }
            }
            else
            {
                player.animationCoolDown--;
                if (player.isMoving)
                    player.animationCoolDown = 0;
            }
        }
    }
}
