using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();

        if (particle == null)
        {
            Debug.Log("파티클 없음");
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        PlayParticleSystem();
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        StopParticleSystem();
    //    }
    //}


}
