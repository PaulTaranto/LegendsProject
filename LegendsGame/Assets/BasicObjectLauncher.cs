using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObjectLauncher : MonoBehaviour
{   
    //just spwns objects on left click

    public GameObject projectilePrefab;
    public float ReloadSpeed = 0.35f;

    private bool TriggerDown = false;
    private bool Shooting = false;

    public Transform Firepoint;
    public float Spread;

    public Animator animator;

    AimMouse aim;


    private void Start()
    {
        InvokeRepeating("Shoot", 0f, 0f);
        aim = GetComponent<AimMouse>();

    }

    void Update()
    {
        InvokeRepeating("Shoot", 0f, 0f);

        //trigger
        if (Input.GetMouseButtonDown(0))
        {
            TriggerDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            TriggerDown = false;
        }
    }

    //Shooting
    private void Shoot()
    {
        if (TriggerDown == true && Shooting == false) {
            StartCoroutine(Blam());
//            animator.SetTrigger("Shoot");
        }
    }

    //Reload
    private IEnumerator Blam()
    {
        Instantiate(projectilePrefab, Firepoint.transform.position, aim.GetAngle());
        Shooting = true;
        yield return new WaitForSeconds(ReloadSpeed);
        Shooting = false;
    }
}

