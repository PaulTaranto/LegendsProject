using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBullet : MonoBehaviour
{
    public float speed;

    float liveTime = 6;    
    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        target = new Vector2(player.position.x, player.position.y);

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            this.DestroyProjectile();
            other.gameObject.GetComponent<Health>().GiveDamage(2);            
        }
    }
    void DestroyProjectile(){

        Destroy(this.gameObject);

    }
}
