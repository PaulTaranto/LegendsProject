using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 10f;
    bool hasControl = true;
    void Update()
    {
        if(hasControl)
        {
            Vector2 move = transform.position;
            move.x += Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            move.y += Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            transform.position = move;
        }
        //Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime);
        //controller.Move(move);
    }

    public void SetPlayerControl(bool controlState)
    {
        hasControl = controlState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Final Item")
        {
            Destroy(collision.gameObject);
            //Play sparkle collecting effect
            //Fade to black
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel("FinalCinematic");
        }
    }
}
