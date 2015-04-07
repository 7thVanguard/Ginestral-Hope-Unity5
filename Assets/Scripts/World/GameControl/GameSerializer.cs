using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[System.Serializable]
public struct PlayerStruct
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float cameraObjective;
    public float cameraAngleSight;
    public bool unlockedSkillFireBall;
}


[System.Serializable]
public struct ChunkStruct
{
    public int chunkNumberX;
    public int chunkNumberY;
    public int chunkNumberZ;
    public int chunkSizeX;
    public int chunkSizeY;
    public int chunkSizeZ;
}


[System.Serializable]
public struct SunStruct
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public float lightColorR;
    public float lightColorG;
    public float lightColorB;
    public float lightColorA;

    public float flareColorR;
    public float flareColorG;
    public float flareColorB;
    public float flareColorA;

    public float ambientLightColorR;
    public float ambientLightColorG;
    public float ambientLightColorB;
    public float ambientLightColorA;

    public float backGroundColorR;
    public float backGroundColorG;
    public float backGroundColorB;
    public float backGroundColorA;

    public float intensity;
}


[System.Serializable]
public struct VoxelStruct
{
    public string name;
    public int number;
}



[System.Serializable]
public class GameSerializer
{
    private PlayerStruct playerStructSave;
    private ChunkStruct chunkStructSave;
    private SunStruct sunStructSave;
    private VoxelStruct voxelStructSave;


    private static List<VoxelStruct> VoxelSave;

    // Voxel RLE Compression related
    private static string actualName;
    private static int actualNumber;


    public void Save(World world, Player player, string saveName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("Assets/Saves/" + saveName + ".save");

        actualNumber = 1;

        VoxelSave = new List<VoxelStruct>();

        Encryptor(world, player, bf, file);
        file.Close();
    }


    public void Load(World world, Player player, string saveName)
    {
        if (File.Exists("Assets/Saves/" + saveName + ".save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("Assets/Saves/" + saveName + ".save", FileMode.Open);

            actualNumber = 1;

            VoxelSave = new List<VoxelStruct>();

            Desencrypter(world, player, bf, file);
            file.Close();

            // Music
            GameMusic.SelectClips(saveName);
            Global.player.playerObj.transform.FindChild("MusicPlayer").GetComponent<AudioSource>().volume = 0;
            GameMusic.fadingIn = true;

            // Orbs
            Global.player.orbsCollected = 0;
        }
    }


    private void Encryptor(World world, Player player, BinaryFormatter bf, FileStream file)
    {
        //+ Player
        // Find all players in game
        GameObject playerOnGame = GameObject.FindGameObjectWithTag("Player");

        playerStructSave.positionX = playerOnGame.transform.position.x;
        playerStructSave.positionY = playerOnGame.transform.position.y + 0.05f;
        playerStructSave.positionZ = playerOnGame.transform.position.z;
        playerStructSave.rotationX = playerOnGame.transform.eulerAngles.x;
        playerStructSave.rotationY = playerOnGame.transform.eulerAngles.y;
        playerStructSave.rotationZ = playerOnGame.transform.eulerAngles.z;
        playerStructSave.cameraObjective = Global.mainCamera.objectivePosition;
        playerStructSave.cameraAngleSight = Global.mainCamera.angleSight;
        playerStructSave.unlockedSkillFireBall = player.unlockedSkillFireBall;

        bf.Serialize(file, playerStructSave);


        //+ Chunks
        chunkStructSave.chunkNumberX = world.chunkNumber.x;
        chunkStructSave.chunkNumberY = world.chunkNumber.y;
        chunkStructSave.chunkNumberZ = world.chunkNumber.z;
        chunkStructSave.chunkSizeX = world.chunkSize.x;
        chunkStructSave.chunkSizeY = world.chunkSize.y;
        chunkStructSave.chunkSizeZ = world.chunkSize.z;

        bf.Serialize(file, chunkStructSave);

        //+ Sun
        sunStructSave.positionX = Global.sun.sunObj.transform.position.x;
        sunStructSave.positionY = Global.sun.sunObj.transform.position.y;
        sunStructSave.positionZ = Global.sun.sunObj.transform.position.z;
        sunStructSave.rotationX = Global.sun.sunObj.transform.eulerAngles.x;
        sunStructSave.rotationY = Global.sun.sunObj.transform.eulerAngles.y;
        sunStructSave.rotationZ = Global.sun.sunObj.transform.eulerAngles.z;

        sunStructSave.lightColorR = Global.sun.light.color.r;
        sunStructSave.lightColorG = Global.sun.light.color.g;
        sunStructSave.lightColorB = Global.sun.light.color.b;
        sunStructSave.lightColorA = Global.sun.light.color.a;

        sunStructSave.flareColorR = Global.sun.lensFlare.color.r;
        sunStructSave.flareColorG = Global.sun.lensFlare.color.g;
        sunStructSave.flareColorB = Global.sun.lensFlare.color.b;
        sunStructSave.flareColorA = Global.sun.lensFlare.color.a;

        sunStructSave.ambientLightColorR = RenderSettings.ambientLight.r;
        sunStructSave.ambientLightColorG = RenderSettings.ambientLight.g;
        sunStructSave.ambientLightColorB = RenderSettings.ambientLight.b;
        sunStructSave.ambientLightColorA = RenderSettings.ambientLight.a;

        sunStructSave.backGroundColorR = Camera.main.backgroundColor.r;
        sunStructSave.backGroundColorG = Camera.main.backgroundColor.g;
        sunStructSave.backGroundColorB = Camera.main.backgroundColor.b;
        sunStructSave.backGroundColorA = Camera.main.backgroundColor.a;

        sunStructSave.intensity = Global.sun.light.intensity;
        bf.Serialize(file, sunStructSave);

        //+ Voxels
        bool firstVoxel = true;

        for (int cx = 0; cx < world.chunkNumber.x; cx++)
            for (int cy = 0; cy < world.chunkNumber.y; cy++)
                for (int cz = 0; cz < world.chunkNumber.z; cz++)
                    if (!world.chunk[cx, cy, cz].empty)
                        for (int x = 0; x < world.chunkSize.x; x++)
                            for (int y = 0; y < world.chunkSize.y; y++)
                                for (int z = 0; z < world.chunkSize.z; z++)
                                {
                                    if (firstVoxel)
                                    {
                                        firstVoxel = false;
                                        actualName = world.chunk[cx, cy, cz].voxel[x, y, z].ID;
                                    }
                                    else
                                    {
                                        if (actualName == world.chunk[cx, cy, cz].voxel[x, y, z].ID)
                                            actualNumber++;
                                        else
                                        {
                                            voxelStructSave.name = actualName;
                                            voxelStructSave.number = actualNumber;
                                            VoxelSave.Add(voxelStructSave);

                                            actualNumber = 1;
                                            actualName = world.chunk[cx, cy, cz].voxel[x, y, z].ID;
                                        }
                                    }
                                }

        // The last struct is not detected in the loop
        voxelStructSave.name = actualName;
        voxelStructSave.number = actualNumber;
        VoxelSave.Add(voxelStructSave);

        // Serialization of the lists
        bf.Serialize(file, VoxelSave);
        VoxelSave.Clear();
    }


    private void Desencrypter(World world, Player player, BinaryFormatter bf, FileStream file)
    {
        //+ Player
        // Deserialize the players
        playerStructSave = (PlayerStruct)bf.Deserialize(file);

        GameObject playerOnGame = GameObject.FindGameObjectWithTag("Player");

        playerOnGame.transform.position = new Vector3(playerStructSave.positionX, playerStructSave.positionY, playerStructSave.positionZ);
        playerOnGame.transform.eulerAngles = new Vector3(playerStructSave.rotationX, playerStructSave.rotationY, playerStructSave.rotationZ);
        player.unlockedSkillFireBall = playerStructSave.unlockedSkillFireBall;


        //+ Chunks
        // Deserialize the chunk struct
        chunkStructSave = (ChunkStruct)bf.Deserialize(file);
        // ------------------------------------------------------------------------------- Converter
        if (world.chunkNumber.x != chunkStructSave.chunkNumberX || world.chunkSize.x != chunkStructSave.chunkSizeX ||
            world.chunkNumber.y != chunkStructSave.chunkNumberY || world.chunkSize.y != chunkStructSave.chunkSizeY ||
            world.chunkNumber.z != chunkStructSave.chunkNumberZ || world.chunkSize.z != chunkStructSave.chunkSizeZ)
        {
            world.chunkNumber.x = chunkStructSave.chunkNumberX;
            world.chunkNumber.y = chunkStructSave.chunkNumberY;
            world.chunkNumber.z = chunkStructSave.chunkNumberZ;
            world.chunkSize.x = chunkStructSave.chunkSizeX;
            world.chunkSize.y = chunkStructSave.chunkSizeY;
            world.chunkSize.z = chunkStructSave.chunkSizeZ;

            // Destroy existing chunks
            GameObject[] chunks = GameObject.FindGameObjectsWithTag("Chunk");
            foreach (GameObject chunkObj in chunks)
                GameObject.Destroy(chunkObj);

            // Reset the world
            world.Init();
        }


        //+ Sun
        sunStructSave = (SunStruct)bf.Deserialize(file);

        Global.sun.sunObj.transform.position = new Vector3(sunStructSave.positionX, sunStructSave.positionY, sunStructSave.positionZ);
        Global.sun.sunObj.transform.eulerAngles = new Vector3(sunStructSave.rotationX, sunStructSave.rotationY, sunStructSave.rotationZ);

        Global.sun.light.color = new Color(sunStructSave.lightColorR, sunStructSave.lightColorG, sunStructSave.lightColorB, sunStructSave.lightColorA);
        Global.sun.lensFlare.color = new Color(sunStructSave.flareColorR, sunStructSave.flareColorG, sunStructSave.flareColorB, sunStructSave.flareColorA);
        RenderSettings.ambientLight = new Color(sunStructSave.ambientLightColorR, sunStructSave.ambientLightColorG, sunStructSave.ambientLightColorB, sunStructSave.ambientLightColorA);
        Camera.main.backgroundColor = new Color(sunStructSave.backGroundColorR, sunStructSave.backGroundColorG, sunStructSave.backGroundColorB, sunStructSave.backGroundColorA);

        Global.sun.light.intensity = sunStructSave.intensity;

        //+ Voxels
        // Deserialize the voxels list
        VoxelSave = (List<VoxelStruct>)bf.Deserialize(file);

        int listPosition = -1;
        int downCounter = 0;

        for (int cx = 0; cx < world.chunkNumber.x; cx++)
            for (int cy = 0; cy < world.chunkNumber.y; cy++)
                for (int cz = 0; cz < world.chunkNumber.z; cz++)
                    if (!world.chunk[cx, cy, cz].empty)
                        for (int x = 0; x < world.chunkSize.x; x++)
                            for (int y = 0; y < world.chunkSize.y; y++)
                                for (int z = 0; z < world.chunkSize.z; z++)
                                {
                                    if (downCounter > 0)
                                    {
                                        world.chunk[cx, cy, cz].voxel[x, y, z] =
                                            new Voxel(world, new IntVector3(x, y, z), new IntVector3(cx, cy, cz), VoxelSave[listPosition].name);

                                        downCounter--;
                                    }
                                    else
                                    {
                                        listPosition++;
                                        downCounter = VoxelSave[listPosition].number - 1;

                                        world.chunk[cx, cy, cz].voxel[x, y, z] =
                                            new Voxel(world, new IntVector3(x, y, z), new IntVector3(cx, cy, cz), VoxelSave[listPosition].name);
                                    }
                                }
        VoxelSave.Clear();

        // Reset voxels
        foreach (Chunk chunk in world.chunk)
        {
            chunk.BuildChunkVertices(world);
            chunk.BuildChunkMesh();
        }
    }
}
