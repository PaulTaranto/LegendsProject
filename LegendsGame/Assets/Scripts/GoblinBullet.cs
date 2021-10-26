using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBullet : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 8;

    float liveTime = 6;    
    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, -rotationSpeed));

        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            this.DestroyProjectile();
            other.gameObject.GetComponent<Health>().GiveDamage(2, transform.position);            
        }
    }
    void DestroyProjectile(){

        Destroy(this.gameObject);

    }
}
