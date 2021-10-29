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

    public Animator animator;

    AudioSource audio;



    void Start()
    {
        timeBtwShots = startTimeBtwShots;

        player = GameObject.FindGameObjectWithTag("Player").transform; 
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();     

        audio = GetComponent<AudioSource>();        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Hole" || other.gameObject.tag == "Slime" || other.gameObject.tag == "Goblin")
        {
            ContactPoint2D[] contacts = new ContactPoint2D[2];
            other.GetContacts(contacts);
            reflectVector();
            //calcuateNewMovementVector();
        }
    }

    void calcuateNewMovementVector()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void reflectVector()
    {
        //movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementDirection = -movementDirection;
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
            // animator.SetBool("Moving", true);


            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
                animator.SetTrigger("Attack");

                audio.Play();
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
