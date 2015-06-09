using UnityEngine;
using System.Collections;

public enum AnimState { MoveDown, MoveUp }
public class NGGH_EndLevel : MonoBehaviour 
{
    private AnimState animState = AnimState.MoveUp;

    private Texture2D sevenV;
    private Texture2D blackSpace;

    private float alphaCounterBlackScreen = 0;
    private float alphaCounter7V = 0;
    private bool active = false;
    private bool inTrigger = false;

    //Fire Gem Transform
    public Transform fireGemTransform;
    private Vector3 fireGemInitialPosition;
    private Transform fireGemAuxTransform;

    private float gemRotationSpeed = -1;

    public float frameToframeAnimSpeed;
    public float animTime;
    private float animTimeInit;
    public float animSlerpTime;


    void Start()
    {
        sevenV = (Texture2D)Resources.Load("UI/7V");
        blackSpace = (Texture2D)Resources.Load("UI/BlackScreen");
        fireGemInitialPosition = fireGemTransform.position;

        fireGemAuxTransform = fireGemTransform;
        //fireGemAuxTransform.position = fireGemInitialPosition;

        animTimeInit = animTime;
    }


    void Update()
    {
        if (active)
        {
            if (alphaCounterBlackScreen < 255)
                alphaCounterBlackScreen += 50 * Time.deltaTime;

            if (alphaCounterBlackScreen > 255)
                alphaCounterBlackScreen = 255;

            if (alphaCounterBlackScreen >= 254)
            {
                if (alphaCounter7V < 255)
                    alphaCounter7V += 50 * Time.deltaTime;

                if (alphaCounter7V > 255)
                    alphaCounter7V = 255;
            }
        }

        if (GameFlow.pause)
        {
            alphaCounterBlackScreen = 0;
            alphaCounter7V = 0;
            active = false;
        }

        AnimateFireGem();
    }


    void OnGUI()
    {
        if (active)
        {
            GUI.color = new Color32(0, 0, 0, (byte)alphaCounterBlackScreen);
            GUI.DrawTextureWithTexCoords(new Rect(0, 0, Screen.width, Screen.height), blackSpace, new Rect(0, 0, 1, 1));

            GUI.color = new Color32(255, 255, 255, (byte)alphaCounter7V);
            GUI.DrawTextureWithTexCoords(new Rect(Screen.width / 4, Screen.height / 3, Screen.width / 2, Screen.height / 6), sevenV, new Rect(0, 0, 1, 1));
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
        fireGemTransform.position = Vector3.Slerp(fireGemTransform.position, fireGemAuxTransform.position, animSlerpTime);

        switch (animState)
        {
            case AnimState.MoveUp:
                {
                    UpBehaviour();

                }break;

            case AnimState.MoveDown:
                {
                    DownBehaviour();

                }break;

            default:
                break;
        }
    }


    #region Animate Gem Auxiliar Functions
    //Set
	    private void SetUp(){

            animTime = animTimeInit;

            animState = AnimState.MoveUp;
		
	    }
	
	    private void SetDown(){

            animTime = animTimeInit;

            animState = AnimState.MoveDown;
	    }
	    //Behaviours
	    private void UpBehaviour(){
            animTime -= Time.deltaTime;
            if (animTime < 0) SetDown();

            fireGemAuxTransform.Translate(new Vector3(0, Time.deltaTime * frameToframeAnimSpeed, 0));
		
	    }
	
	    private void DownBehaviour(){
            animTime -= Time.deltaTime;
            if (animTime < 0) SetUp();

            fireGemAuxTransform.Translate(new Vector3(0, Time.deltaTime * -frameToframeAnimSpeed, 0));

        }

    #endregion
}
