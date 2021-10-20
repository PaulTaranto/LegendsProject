using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
//    private float angle = 0;
    public GameObject fliped;
    public bool isFacingRight = true;

    private float ShootTime = 1f;
    public float MinNum;
    public float MaxNum;
    public Transform Firepoint;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        while (ShootTime <= 0)
        {
            ShootTime = Random.Range(MinNum, MaxNum);
            yield return new WaitForSeconds(ShootTime);
            Instantiate(projectilePrefab, Firepoint.transform.position, transform.rotation);
        }
    }

    private GameObject target;
    // Update is called once per frame
    void Update()
    {


        //rotation
        //look at mouse
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg;

        //for the set dirrection
        if (angle <= 22.5 && angle > -22.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (angle <= 67.5 && angle > 22.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
        }
        if (angle <= 112.5 && angle > 67.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        if (angle <= 157.5 && angle > 112.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
        }
        if (angle <= 180 && angle > 157.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        if (angle >= -180 && angle < -157.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        if (angle >= -157.5 && angle < -112.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 225));
        }
        if (angle >= -112.5 && angle < -67.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
        if (angle >= -67.5 && angle < -22.5)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
        }

        //Flip
        if (angle <= 90 && angle > -90)
        {
            fliped.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (angle <= 180 && angle > 90)
        {
            fliped.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (angle >= -180 && angle < -90)
        {
            fliped.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
