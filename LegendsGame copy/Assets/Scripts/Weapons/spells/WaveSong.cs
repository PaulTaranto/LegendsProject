using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSong : MonoBehaviour
{
    public float forceApplied;
    private int speed = 1;
    public float DestroyTime;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")Debug.Log("Collision!");
        {
            GetComponent<Rigidbody>().AddForce(0, forceApplied, 0);
        }
    }
    private void Start()
    {
        StartCoroutine(Delete());
    }

    public IEnumerator Delete()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }
}
