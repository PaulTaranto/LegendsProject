﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySpawn1 : MonoBehaviour
{
    public GameObject[] toSpawn;

    //private void Start()
    //{
    //    spawnEnemy(Entity.DRAGON, new Vector3(0, 0, 0));
    //}

    public void SpawnRandomEnemy()
    {
        int index = Random.Range(0, toSpawn.Length);
        Instantiate(toSpawn[index], toSpawn[index].transform.position, toSpawn[index].transform.rotation);
    }

    #region SpawnEnemy
    public void SpawnEnemy(int arrayIndex)
    {
        Instantiate(toSpawn[arrayIndex], toSpawn[arrayIndex].transform.position, toSpawn[arrayIndex].transform.rotation);
    }

    public void SpawnEnemy(int arrayIndex, Vector3 position)
    {
        Instantiate(toSpawn[arrayIndex], position, toSpawn[arrayIndex].transform.rotation);
    }

    public void SpawnEnemy(int arrayIndex, Vector3 position, Quaternion rotation)
    {
        Instantiate(toSpawn[arrayIndex], position, rotation);
    }

    public void SpawnEnemy(Entity entity)
    {
        Instantiate(toSpawn[(int)entity], toSpawn[(int)entity].transform.position, toSpawn[(int)entity].transform.rotation);
    }

    public void SpawnEnemy(Entity entity, Vector3 position)
    {
        Instantiate(toSpawn[(int)entity], position, toSpawn[(int)entity].transform.rotation);
    }

    public void SpawnEnemy(Entity entity, Vector3 position, Quaternion rotation)
    {
        Instantiate(toSpawn[(int)entity], position, rotation);
    }
    #endregion
}

public enum Entity
{
    //Order of this must fit order of toSpawn array
    DRAGON,
    GOBLIN,
    SLIME
}
