using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosion : MonoBehaviour
{
    //Used as a quick collider for damage
    void Start()
    {
        Destroy(gameObject);
    }
}
