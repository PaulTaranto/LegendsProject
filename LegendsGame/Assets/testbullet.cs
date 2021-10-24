using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbullet : MonoBehaviour
{
    float timeToLive = 5;
    float moveRate = 30;
    int bulletDamage = 10;

    private void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    { 
        Vector2 move = transform.position;
        move.x += moveRate * Time.fixedDeltaTime;
        transform.position = move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon")
        {
            Health health = collision.transform.root.GetComponentInChildren<Health>();
            health.GiveDamage(bulletDamage, transform.position);
            Destroy(gameObject);//Destrosy the bullet
        }
        if (collision.gameObject.tag == "slime")
        {
            Health health = collision.transform.root.GetComponentInChildren<Health>();
            health.GiveDamage(bulletDamage, transform.position);
            Destroy(gameObject);
        }
    }
}
