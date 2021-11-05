using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    float timer, maxTimer = 10;
    bool right = true;
    bool finishedMoving = true;

    float temp;

    private void Start()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log(timer);

        if(!finishedMoving)
        {
            temp += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(timer < 0)
        {
            finishedMoving = false;
            if (right)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        if (transform.position.x > 12 && !finishedMoving)
        {
            temp = 0;
            finishedMoving = true;
            right = false;
            timer = maxTimer;
        }
        else if (transform.position.x < -12 && !finishedMoving)
        {
            finishedMoving = true;
            right = true;
            timer = maxTimer;
        }
    }

    void MoveRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector2 move = transform.position;
        move.x += moveSpeed * Time.fixedDeltaTime;

        animator.SetBool("Moving", true);

        transform.position = move;
    }

    void MoveLeft()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        Vector2 move = transform.position;
        move.x -= moveSpeed * Time.fixedDeltaTime;

        animator.SetBool("Moving", true);

        transform.position = move;
    }
}
