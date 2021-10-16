using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    bool isDestroyed = false;

    public void SetDestoryed(bool b)
    {
        isDestroyed = b;
    }

    private void Update()
    {
        if(isDestroyed)
        {
            GameObject.Find("ArraySpawner").GetComponent<ArraySpawn1>().SpawnRandomEnemy(this.gameObject.transform.position);
        }
    }
}
