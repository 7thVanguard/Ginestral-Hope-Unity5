﻿using UnityEngine;
using System.Collections;

public class DevConstructionToolsManager
{
    // Detecting variables
    // Base detections
    public static Chunk chunk;
    public static Voxel voxel;

    // Auxiliar
    public static Chunk detChunk;
    public static Voxel detVoxel;
    public static Vector2 detVertex;

    // Multi selection tools
    public static bool selected = false;

    // Number of the vertex: 0 - backLeft, 1 - backRight, 2 - frontRight, 3 - frontLeft
    private static int voxelVertex;


    // Math variables
    private static int sedimentExcess;
    private static int sedimentPerClick = 3;

    public void Update(World world, Player player, MainCamera mainCamera)
    {
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Remove(world, player, mainCamera);
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                Place(world, player, mainCamera);
            else if (Input.GetKeyDown(KeyCode.Mouse2))
                Cancel();
            else
                Detect(world, player, mainCamera);
        }
    }


    private void Remove(World world, Player player, MainCamera mainCamera)
    {
        if (mainCamera.raycast.transform.tag == "Chunk")
            VoxelsToolManager.Remove(world, player, mainCamera);
        else if (GameFlow.selectedTool == GameFlow.SelectedTool.EVENT)
            EventsToolManager.Remove(world, player, mainCamera);
    }


    private void Place(World world, Player player, MainCamera mainCamera)
    {
        if (mainCamera.raycast.transform.tag == "Chunk")
        {
            if (GameFlow.selectedTool == GameFlow.SelectedTool.VOXEL)
                VoxelsToolManager.Place(world, player, mainCamera);
            else if (GameFlow.selectedTool == GameFlow.SelectedTool.EVENT)
                EventsToolManager.Place(world, player, mainCamera);
        }
    }


    private void Cancel()
    {
        if (GameFlow.selectedTool == GameFlow.SelectedTool.VOXEL)
            VoxelsToolManager.Cancel();
    }


    private void Detect(World world, Player player, MainCamera mainCamera)
    {
        if (GameFlow.selectedTool == GameFlow.SelectedTool.VOXEL)
            VoxelsToolManager.Detect(world, player, mainCamera);
        else if (GameFlow.selectedTool == GameFlow.SelectedTool.EVENT)
            EventsToolManager.Detect(world, player, mainCamera);
    }
}
