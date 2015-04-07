﻿using UnityEngine;
using System.Collections;

public static class Global
{
    public static World world;
    public static Player player;
    public static MainCamera mainCamera;

    public static GameObject verticalLight;

    public static Sun sun;

    public static Material G1 = (Material)Resources.Load("Atlas/GHAtlasMat");
    public static Material P1 = (Material)Resources.Load("Atlas/Terrain Atlas/PDAtlas1Mat");
}
