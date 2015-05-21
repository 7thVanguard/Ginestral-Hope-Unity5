using UnityEngine;
using System.Collections;

public class C_NewGame : MonoBehaviour 
{
    void Update()
    {
        if (GameFlow.resetState == GameFlow.ResetState.Reset)
        {
            Global.player.playerObj.transform.position = new Vector3(3, 1, 4);
            Global.player.playerObj.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
}
