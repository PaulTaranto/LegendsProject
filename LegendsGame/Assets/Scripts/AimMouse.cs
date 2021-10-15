using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AimMouse : MonoBehaviour
{
    private float angle = 0;
    public GameObject fliped;
    public bool isFacingRight = true;

    void Update()
    {
        //rotation
        //look at mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
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
    void Flip()
    {
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
