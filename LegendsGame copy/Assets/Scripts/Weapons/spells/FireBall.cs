using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 6;
    private float DestroyTime = 10;

    public GameObject explosion;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    private void Start()
    {
        //StartCoroutine(Shoot());
        StartCoroutine(Delete());
    }

    public IEnumerator Delete()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
