using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySpawn : MonoBehaviour
{
    public GameObject[] ToSpawn;
    private int SpawnNumber;

    void Start()
    {
        SpawnNumber = Random.Range(0, ToSpawn.Length);
        Instantiate(ToSpawn[SpawnNumber], transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
