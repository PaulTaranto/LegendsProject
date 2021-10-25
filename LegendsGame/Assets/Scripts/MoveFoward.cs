using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    public float speed = 40;
    private Rigidbody playerRb;
    private Collider m_Collider;
    public float DestroyedTimer = 3;
    void Start()
    {
        GameObject Rotator = GameObject.Find("Weapons & Rotator");
        playerRb = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        StartCoroutine(Limit());
    }
    void Update()
    {
        //just to walk forward
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon")
        {
            Debug.Log("cawk");
            Health health = collision.transform.root.GetComponentInChildren<Health>();
            Debug.Log(health.gameObject);
            health.GiveDamage(1);
            Destroy(gameObject);//Destrosy the bullet
        }
    }



    private IEnumerator Limit()
    {
        yield return new WaitForSeconds(DestroyedTimer);
        Destroy(gameObject);
    }
}
