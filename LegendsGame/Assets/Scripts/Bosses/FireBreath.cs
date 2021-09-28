using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour
{
    public ParticleSystem part;

    public int fireDamage = 1;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().GiveDamage(fireDamage);
        }
    }

}
