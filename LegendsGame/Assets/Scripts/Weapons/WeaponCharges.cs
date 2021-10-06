using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharges : MonoBehaviour
{
    public GameObject Firepoint;
    public float time = 6;
    private bool BLJ = true;
    private float Minb = 0;
    public GameObject Bar;
    public int charges;
    private int maxCharges;
    public bool activate = false;
    public GameObject projectilePrefab;
    private void Start()
    {
        maxCharges = charges;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && BLJ)
        {
            Instantiate(projectilePrefab, Firepoint.transform.position, transform.rotation);
            charges -= 1;
            activate = true;
            if (charges > 0)
            {
                BLJ = false;
            }
            StartCoroutine(Reloading());
        }
    }
    private IEnumerator Reloading()
    {
        while (charges < maxCharges)
        {
            Minb = 0;
            while (Minb < 100)
            {
                yield return new WaitForSeconds(time / 100);
                Minb = Minb + 1;
                Bar.gameObject.transform.localScale = new Vector3(Minb / 100, Minb / 100, Minb / 100);
            }
            BLJ = true;
            charges += 1;
        }
    }
}
