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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayParticleSystem();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopParticleSystem();
        }
    }

    void PlayParticleSystem()
    {
        if(particle != null)
        {
            SpawnParticleMousePosition();
            particle.Play();
        }
    }
    void StopParticleSystem()
    {
        if(particle != null)
        {
            particle.Stop();
        }
    }

    void SpawnParticleMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Camera.main.nearClipPlane + 1f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        particle.transform.position = worldPosition;

        particle.Play();
    }
}
