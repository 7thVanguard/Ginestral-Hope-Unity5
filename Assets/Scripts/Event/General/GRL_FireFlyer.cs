using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRL_FireFlyer : MonoBehaviour 
{
    public enum RouteType { Circular, goAndReverse, Random }
    public RouteType routeType;

    public List<Vector3> ControlPoints = new List<Vector3>();

    public float speed = 3;
    public float stationaryTime = 2;

    [HideInInspector] public bool emitting = false;

    private float routeTimeCounter = 0;
    private float stationaryTimeCounter = 0;
    private int positionIndex = 1;
    private bool stationary = false;
    private bool returning = false;


    void Update()
    {
        if (emitting)
        {
            Debug.Log("hit");
            emitting = false;
        }
        else
        {
            if (!stationary)
            {
                switch (routeType)
                {
                    case RouteType.Circular:
                        {
                            routeTimeCounter += Time.deltaTime / speed;
                            if (routeTimeCounter >= 1)
                            {
                                routeTimeCounter = 0;

                                positionIndex++;
                                if (positionIndex == ControlPoints.Count)
                                    positionIndex = 0;

                                stationary = true;
                            }

                            if (positionIndex == ControlPoints.Count - 1)
                                transform.position = Vector3.Lerp(ControlPoints[positionIndex], ControlPoints[0], routeTimeCounter);
                            else
                                transform.position = Vector3.Lerp(ControlPoints[positionIndex], ControlPoints[positionIndex + 1], routeTimeCounter);
                        }
                        break;
                    case RouteType.goAndReverse:
                        break;
                    case RouteType.Random:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                stationaryTimeCounter += Time.deltaTime;
                if (stationaryTimeCounter >= stationaryTime)
                {
                    stationaryTimeCounter = 0;
                    stationary = false;
                }
            }
        }
    }
}
