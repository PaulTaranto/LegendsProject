using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangedgoblin : MonoBehaviour
{

    private Transform player;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;


    void Start()
    {
        timeBtwShots = startTimeBtwShots;

        player = GameObject.FindGameObjectWithTag("Player").transform; 
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();             

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            calcuateNewMovementVector();
        }
        //if (other.CompareTag("WallEast"))
        //{
        //    calcuateNewMovementVector();
        //}
        //if (other.CompareTag("WallSouth"))
        //{
        //    calcuateNewMovementVector();
        //}
        //if (other.CompareTag("WallWest"))
        //{
        //    calcuateNewMovementVector();
        //}
    }

    void calcuateNewMovementVector()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Constants.mapManager.isTransitioning)
        {
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }

            //move the goblin 
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));


            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}