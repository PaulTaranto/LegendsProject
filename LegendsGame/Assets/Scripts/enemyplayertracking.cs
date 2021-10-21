using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyplayertracking : MonoBehaviour
{

    public float speed = 0.7f;
    private Transform player;
    private Vector2 target;    

    public Animator animator;

    void Start()
    {

            
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            animator.SetBool("Moving", false);
        }

        if (!Constants.mapManager.isTransitioning)
        {
            target = new Vector2(player.position.x, player.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            animator.SetBool("Moving", true);
        }
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<Health>().GiveDamage(2);
            animator.SetTrigger("Attack");
        }

    }    
}
