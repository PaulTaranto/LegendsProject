using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecharge : MonoBehaviour
{
    public GameObject Firepoint;
    public float time = 6;
    private bool BLJ = true;
    private float Minb = 0;
    public GameObject Bar;
    public GameObject projectilePrefab;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && BLJ)
        {
            BLJ = false;
            StartCoroutine(Reloading());
        }
    }
    private IEnumerator Reloading()
    {
        Minb = 0;
        while (Minb < 100)
        {
            Instantiate(projectilePrefab, Firepoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(time / 100);
            Minb = Minb + 1;
            Bar.gameObject.transform.localScale = new Vector3(Minb / 100, Minb / 100, Minb / 100);
        }
        BLJ = true;
    }
}
