using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 10f;
    //CharacterController controller;
    private void Start()
    {
        //controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Vector2 move = transform.position;
        move.x += Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        move.y += Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.position = move;
        //Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime);
        //controller.Move(move);
    }
}
