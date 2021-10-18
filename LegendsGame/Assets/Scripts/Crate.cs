using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    bool isDestroyed = false;

    private void FixedUpdate()
    {
        //TODO give collision with bullet functionality
        if(Input.GetKey(KeyCode.A))
        {
            SetDestroyed();
        }
    }

    public void SetDestroyed()
    {
        if(!isDestroyed)
        {
            GameObject.Find("ArraySpawner").GetComponent<ArraySpawn1>().SpawnRandomEnemy(this.gameObject.transform.position);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            GetComponent<Animator>().SetBool("destroyed", true);

            isDestroyed = true;
        }
    }
}
