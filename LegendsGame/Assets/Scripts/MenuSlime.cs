using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlime : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    float timer, maxTimer = 20 + 4.743708f;// 9.487416f;
    bool finishedMoving = true;

    private void Start()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log(timer);
    }

    void FixedUpdate()
    {
        if (timer < 0)
        {
            finishedMoving = false;
            MoveLeft();
        }

        if (transform.position.x < -12 && !finishedMoving)
        {
            transform.position = new Vector3(14, -4.41f, 0);
            finishedMoving = true;
            timer = maxTimer;
        }
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
