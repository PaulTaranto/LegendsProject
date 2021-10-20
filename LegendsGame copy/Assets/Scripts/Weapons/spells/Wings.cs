using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
    public float time = 6;
    public float ActiveT = 10;
    private bool BLJ = true;
    private float Minb = 0;
    public GameObject Bar;

    public GameObject player;
    private PlayerMovement movement; //Needs to be the movement script

    private void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && BLJ)
        {
            BLJ = false;
            StartCoroutine(Activ());
        }
    }
    private IEnumerator Activ() {
        movement.moveSpeed *= 2;
        yield return new WaitForSeconds(time / 100);
        movement.moveSpeed /= 2;
        StartCoroutine(Reloading());
    }
    private IEnumerator Reloading()
    {
        Minb = 0;
        while (Minb < 100)
        {
            yield return new WaitForSeconds(time / 100);
            Minb = Minb + 1;
            Bar.gameObject.transform.localScale = new Vector3(Minb / 100, Minb / 100, Minb / 100);
        }
        BLJ = true;
    }
}