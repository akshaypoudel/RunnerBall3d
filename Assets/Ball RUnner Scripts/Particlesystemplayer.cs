using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particlesystemplayer : MonoBehaviour
{
    public ParticleSystem stars;
    // Start is called before the first frame update
    void Start()
    {
        stars.Play();
    }
}
