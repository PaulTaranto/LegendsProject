using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    bool isDestroyed = false;
    float randomChance = 0.1f;
    Animator animator;
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();  
    }    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //animator.SetBool("destroyed", isDestroyed);
    }

    private void FixedUpdate()
    {
        if(animator.GetBool("destroyed") != isDestroyed)
        {
            animator.SetBool("destroyed", isDestroyed);
        }
    }

    public void SetDestroyed()
    {
        if(!isDestroyed)
        {
            if(Random.Range(0f,1f) <= randomChance)
            {
                GameObject.Find("ArraySpawner").GetComponent<ArraySpawn1>().SpawnRandomEnemy(this.gameObject.transform.position);
            }
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            isDestroyed = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Bullet")
        {
            Destroy(collision.gameObject);
            SetDestroyed();
            audio.Play();            
        }
    }
}
