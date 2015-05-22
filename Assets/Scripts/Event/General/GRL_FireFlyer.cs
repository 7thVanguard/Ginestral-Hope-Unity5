using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRL_FireFlyer : MonoBehaviour 
{
    public enum RouteType { Circular, goAndReverse, Random }
    public RouteType routeType;

    public List<Vector3> ControlPoints = new List<Vector3>();

    public float speed = 3;
    public bool emitting = false;

    private float timeCounter;
    private int positionIndex = 1;
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
            switch (routeType)
            {
                case RouteType.Circular:
                    {
                        timeCounter += Time.deltaTime / speed;
                        if (timeCounter >= 1)
                        {
                            timeCounter = 0;

                            positionIndex++;
                            if (positionIndex == ControlPoints.Count)
                                positionIndex = 0;
                        }

                        if (positionIndex == ControlPoints.Count - 1)
                            transform.position = Vector3.Lerp(ControlPoints[positionIndex], ControlPoints[0], timeCounter);
                        else
                            transform.position = Vector3.Lerp(ControlPoints[positionIndex], ControlPoints[positionIndex + 1], timeCounter);
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
    }
}
