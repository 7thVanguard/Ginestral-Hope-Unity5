using UnityEngine;
using System.Collections;

public static class Global
{
    public static World world;
    public static Player player;
    public static MainCamera mainCamera;

    public static GameObject verticalLight;

    public static Sun sun;

    public static Material MineAtlas = (Material)Resources.Load("Atlas/MineAtlasMat");
    public static Material FireAtlas = (Material)Resources.Load("Atlas/FireAtlasMat");
}
