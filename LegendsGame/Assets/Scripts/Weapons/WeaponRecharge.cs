using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecharge : MonoBehaviour
{
    public GameObject Firepoint;
    public float time = 6;
//    private bool BLJ = true;
    private float Minb = 0;
    public GameObject Bar;
    public GameObject projectilePrefab;
    AimMouse aim;
    BasicObjectLauncher objectLauncher;
    private void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AimMouse>();
        objectLauncher = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BasicObjectLauncher>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
            //BLJ = false;
            //StartCoroutine(Reloading());
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, objectLauncher.Firepoint.transform.position, aim.GetAngle());
    }


    private IEnumerator Reloading()
    {
        Minb = 0;
        while (Minb < 100)
        {
            Debug.Log("est");
            Instantiate(projectilePrefab, Firepoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(time / 100);
            Minb = Minb + 1;
//            Bar.gameObject.transform.localScale = new Vector3(Minb / 100, Minb / 100, Minb / 100);
        }
//        BLJ = true;
    }
}
