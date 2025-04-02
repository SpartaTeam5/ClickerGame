using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] ParticleSystem normalParticle; // 일반 공격 
    [SerializeField] ParticleSystem criticalEffect; // 치명타

    void Start()
    {
        if (normalParticle == null || criticalEffect == null)
        {
            Debug.LogError("파티클 시스템이 설정되지 않음. 인스펙터에서 확인");
        }
    }
    public void PlayParticleSystem(bool isCritical)
    {
        if (normalParticle != null) normalParticle.Stop();
        if (criticalEffect != null) criticalEffect.Stop();

        ParticleSystem effectPlay = isCritical ? criticalEffect : normalParticle;

        if (effectPlay != null)
        {
            SpawnParticleMousePosition(effectPlay);
            effectPlay.Play();
        }
    }

    void SpawnParticleMousePosition(ParticleSystem particle)
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Camera.main.nearClipPlane + 1f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        particle.transform.position = worldPosition;

        particle.Play();
    }
}