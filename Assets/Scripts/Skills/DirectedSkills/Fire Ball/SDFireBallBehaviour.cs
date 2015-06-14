using UnityEngine;
using System.Collections;

public class SDFireBallBehaviour : MonoBehaviour 
{
    World world;
    RaycastHit impact;

    // Trajectory
    private Vector3 direction;
    private bool isColliding;
    private bool launchedByPlayer;

    // Fire
    private float damage;
    private int blastRadius;

    // Explosion
    private float initialxplosionCounter = 1.66f;
    private float explosionCounter;

    private Light light;
    private float maxLightIntensity = 4;
    private float maxLightRange = 15;

    // Trail
    private bool updateActivation = false;


    private float timeCounter = 20;

    public void Init(World world, Vector3 direction, float damage, int blastRadius)
    {
        this.world = world;
        this.damage = damage;
        this.blastRadius = blastRadius;
        this.direction = direction;
        isColliding = false;

        // Explosion relative
        transform.FindChild("Explosion").gameObject.SetActive(false);
        transform.FindChild("Trail").gameObject.SetActive(false);
        explosionCounter = initialxplosionCounter;
    }


	void Update () 
    {
        if (!GameFlow.pause)
        {
            if (!isColliding)
            {
                //+ Impact detection by raycast
                if (Physics.Raycast(transform.position, direction, out impact, 0.5f))
                    if (impact.transform.gameObject.tag != "Skill")
                        Impact(impact.transform.gameObject);

                //+ Normal movement
                // if the raycast don't detect a collision
                if (!isColliding)
                {
                    // The position of the fireBall without the y component
                    transform.position += direction * Time.deltaTime;

                    // Gravity
                    direction.y -= GamePhysics.gravity * Time.deltaTime;
                    

                    // Activate the trail in the second frame, avoiding a trail glitch
                    if (!updateActivation)
                    {
                        updateActivation = true;
                        transform.FindChild("Trail").gameObject.SetActive(true);
                    }

                    // Destroy depending on the elapsed time
                    timeCounter -= Time.deltaTime;
                    if (timeCounter <= 0)
                        Destroy(this.gameObject);
                    
                }
            }
            
            //+ Explosion
            else
            {
                // Deactivate on impact
                if (explosionCounter == initialxplosionCounter)
                {
                    Destroy(transform.FindChild("Ball").gameObject);
                    Destroy(transform.FindChild("Glow").gameObject);
                    //Destroy(transform.FindChild("Light").gameObject);
                    Destroy(transform.FindChild("Trail").gameObject);

                    // Explosion relative
                    transform.FindChild("Explosion").gameObject.SetActive(true);
                    transform.FindChild("Explosion").GetComponent<AudioSource>().Play();
                    //light = transform.FindChild("Explosion").GetComponent<Light>();
                }

                // Explosion relative
                explosionCounter -= Time.deltaTime;
                //light.intensity = maxLightIntensity * explosionCounter / initialxplosionCounter;
                //light.range = maxLightRange * explosionCounter / initialxplosionCounter;


                if (explosionCounter <= 0)
                    Destroy(gameObject);
            }
        }
	}


    //+ Impact detection by rigid body
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != "Skill")
            Impact(other.transform.gameObject);
    }


    private void Impact(GameObject impact)
    {
        // test if already collided (bug solver)
        if (isColliding)
            return;
        isColliding = true;

        // Impact result
        if (impact.tag == "Enemy")
            impact.transform.gameObject.GetComponent<EnemyComponent>().Damage(damage);
        else if (impact.tag == "Player")
            impact.transform.gameObject.GetComponent<PlayerComponent>().Damage(damage);
        else if (impact.tag == "Fire")
        {
            if (impact.transform.parent.GetComponent<GRL_FireEmitter>() != null)
                impact.transform.parent.GetComponent<GRL_FireEmitter>().emitting = true;
            else if (impact.transform.parent.GetComponent<GRL_FireFlyer>() != null)
                impact.transform.parent.GetComponent<GRL_FireFlyer>().emitting = true;
            
        }
    }
}
