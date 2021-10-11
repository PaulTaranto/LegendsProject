using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyplayertracking : MonoBehaviour
{

    public float speed = 0.7f;
    private Transform player;
   private Vector2 target;    

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);    
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player")){
            GetComponent<Health>().GiveDamage(2);
        }    
    }    
}
