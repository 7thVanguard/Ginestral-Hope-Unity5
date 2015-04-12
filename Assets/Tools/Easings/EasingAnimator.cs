using UnityEngine;
using System.Collections;

public class EasingAnimator : MonoBehaviour
{

    private float currentValue;
    private float timeCounter;
    private bool onEasing;
	
	public Easing.EasingTargetRelativeSpace easingTargetRelativeSpace;
    public Easing.EasingTarget easingTarget;
    public Easing.EasingFunctionType easingFunctionType;

	public float initialValue;
	public float finalValue;
    public float timeDuration;
    public float timeDelay;


	void Start () 
    {
        timeCounter = 0;
		currentValue = initialValue;
        onEasing = true;
	}

	
	void Update () 
    {
        if (onEasing)
            EasingsUpdate();
	}


    public void StartEasing()
    {
        timeCounter = 0;
        onEasing = true;
    }


    void EasingsUpdate()
    {
        if (timeDelay > 0) timeDelay -= Time.deltaTime;
        else if (timeCounter < timeDuration)
        {
            timeCounter += Time.deltaTime;
            currentValue = (float)Easing.BounceEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);

            #region Easings List
            switch (easingFunctionType)
            {
                case Easing.EasingFunctionType.BackEaseIn:
                    {
                        currentValue = (float)Easing.BackEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BackEaseInOut:
                    {
                        currentValue = (float)Easing.BackEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BackEaseOut:
                    {
                        currentValue = (float)Easing.BackEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BackEaseOutIn:
                    {
                        currentValue = (float)Easing.BackEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BounceEaseIn:
                    {
                        currentValue = (float)Easing.BounceEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BounceEaseInOut:
                    {
                        currentValue = (float)Easing.BounceEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BounceEaseOut:
                    {
                        currentValue = (float)Easing.BounceEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.BounceEaseOutIn:
                    {
                        currentValue = (float)Easing.BounceEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CircEaseIn:
                    {
                        currentValue = (float)Easing.CircEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CircEaseInOut:
                    {
                        currentValue = (float)Easing.CircEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CircEaseOut:
                    {
                        currentValue = (float)Easing.CircEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CircEaseOutIn:
                    {
                        currentValue = (float)Easing.CircEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CubicEaseIn:
                    {
                        currentValue = (float)Easing.CubicEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CubicEaseInOut:
                    {
                        currentValue = (float)Easing.CubicEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CubicEaseOut:
                    {
                        currentValue = (float)Easing.CubicEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.CubicEaseOutIn:
                    {
                        currentValue = (float)Easing.CubicEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ElasticEaseIn:
                    {
                        currentValue = (float)Easing.ElasticEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ElasticEaseInOut:
                    {
                        currentValue = (float)Easing.ElasticEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ElasticEaseOut:
                    {
                        currentValue = (float)Easing.ElasticEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ElasticEaseOutIn:
                    {
                        currentValue = (float)Easing.ElasticEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ExpoEaseIn:
                    {
                        currentValue = (float)Easing.ExpoEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ExpoEaseInOut:
                    {
                        currentValue = (float)Easing.ExpoEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ExpoEaseOut:
                    {
                        currentValue = (float)Easing.ExpoEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.ExpoEaseOutIn:
                    {
                        currentValue = (float)Easing.ExpoEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.Linear:
                    {
                        currentValue = (float)Easing.Linear(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuadEaseIn:
                    {
                        currentValue = (float)Easing.QuadEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuadEaseInOut:
                    {
                        currentValue = (float)Easing.QuadEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuadEaseOut:
                    {
                        currentValue = (float)Easing.QuadEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuadEaseOutIn:
                    {
                        currentValue = (float)Easing.QuadEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuartEaseIn:
                    {
                        currentValue = (float)Easing.QuartEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuartEaseInOut:
                    {
                        currentValue = (float)Easing.QuartEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuartEaseOut:
                    {
                        currentValue = (float)Easing.QuartEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuartEaseOutIn:
                    {
                        currentValue = (float)Easing.QuartEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuintEaseIn:
                    {
                        currentValue = (float)Easing.QuintEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuintEaseInOut:
                    {
                        currentValue = (float)Easing.QuintEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuintEaseOut:
                    {
                        currentValue = (float)Easing.QuintEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.QuintEaseOutIn:
                    {
                        currentValue = (float)Easing.QuintEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.SineEaseIn:
                    {
                        currentValue = (float)Easing.SineEaseIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.SineEaseInOut:
                    {
                        currentValue = (float)Easing.SineEaseInOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.SineEaseOut:
                    {
                        currentValue = (float)Easing.SineEaseOut(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;

                case Easing.EasingFunctionType.SineEaseOutIn:
                    {
                        currentValue = (float)Easing.SineEaseOutIn(timeCounter, initialValue, (finalValue - initialValue), timeDuration);
                    } break;
            }
            //End Switch Here
            #endregion

            #region Relative Space Transformations
            if (easingTargetRelativeSpace == Easing.EasingTargetRelativeSpace.WORLD)
                switch (easingTarget)
                {
                    case Easing.EasingTarget.PositionX:
                        {
                            transform.position = new Vector3(currentValue, transform.position.y, transform.position.z);
                        } break;

                    case Easing.EasingTarget.PositionY:
                        {
                            transform.position = new Vector3(transform.position.x, currentValue, transform.position.z);
                        } break;

                    case Easing.EasingTarget.PositionZ:
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y, currentValue);
                        } break;

                    case Easing.EasingTarget.RotationX:
                        {
                            transform.rotation = Quaternion.Euler(currentValue, transform.eulerAngles.y, transform.eulerAngles.z);
                        } break;

                    case Easing.EasingTarget.RotationY:
                        {
                            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, currentValue, transform.eulerAngles.z);
                        } break;

                    case Easing.EasingTarget.RotationZ:
                        {
                            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentValue);
                        } break;

                    case Easing.EasingTarget.ScaleX:
                        {
                            Debug.Log("Unity Engine doesn't have 'WORLD Scale'");
                        } break;

                    case Easing.EasingTarget.ScaleY:
                        {
                            Debug.Log("Unity Engine doesn't have 'WORLD Scale'");
                        } break;

                    case Easing.EasingTarget.ScaleZ:
                        {
                            Debug.Log("Unity Engine doesn't have 'WORLD Scale'");
                        } break;

                    case Easing.EasingTarget.ScaleALL:
                        {
                            Debug.Log("Unity Engine doesn't have 'WORLD Scale'");
                        } break;
                }

            else if (easingTargetRelativeSpace == Easing.EasingTargetRelativeSpace.LOCAL)
                switch (easingTarget)
                {
                    case Easing.EasingTarget.PositionX:
                        {
                            transform.localPosition = new Vector3(currentValue, transform.localPosition.y, transform.localPosition.z);
                        } break;

                    case Easing.EasingTarget.PositionY:
                        {
                            transform.localPosition = new Vector3(transform.position.x, currentValue, transform.localPosition.z);
                        } break;

                    case Easing.EasingTarget.PositionZ:
                        {
                            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentValue);
                        } break;

                    case Easing.EasingTarget.RotationX:
                        {
                            transform.localRotation = Quaternion.Euler(currentValue, transform.localEulerAngles.y, transform.localEulerAngles.z);
                        } break;

                    case Easing.EasingTarget.RotationY:
                        {
                            transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, currentValue, transform.localEulerAngles.z);
                        } break;

                    case Easing.EasingTarget.RotationZ:
                        {
                            transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentValue);
                        } break;

                    case Easing.EasingTarget.ScaleX:
                        {
                            transform.localScale = new Vector3(currentValue, transform.localScale.y, transform.localScale.z);
                        } break;

                    case Easing.EasingTarget.ScaleY:
                        {
                            transform.localScale = new Vector3(transform.localScale.x, currentValue, transform.localScale.z);
                        } break;

                    case Easing.EasingTarget.ScaleZ:
                        {
                            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, currentValue);
                        } break;
                    case Easing.EasingTarget.ScaleALL:
                        {
                            transform.localScale = new Vector3(currentValue, currentValue, currentValue);
                        } break;
                }
            #endregion
        }
        else
            onEasing = false;
    }
}
