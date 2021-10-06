using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyplayertracking : MonoBehaviour
{

    private Transform player;

    public int touchDamage = 1;
    public float speed = 0.7f;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float slimeSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta*slimeSpeed);

    }

}
