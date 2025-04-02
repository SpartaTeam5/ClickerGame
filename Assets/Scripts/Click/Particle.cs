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
    public void PlayParticleSystem(bool isCritical, Vector3 position)
    {
        if (normalParticle != null) normalParticle.Stop();
        if (criticalEffect != null) criticalEffect.Stop();

        ParticleSystem effectPlay = isCritical ? criticalEffect : normalParticle;

        if (effectPlay != null)
        {
            SpawnParticlePosition(effectPlay, position); // 수정된 함수 호출
            effectPlay.Play();
        }
    }

    void SpawnParticlePosition(ParticleSystem particle, Vector3 monsterPosition)
    {
        particle.transform.position = monsterPosition; // 몬스터 위치에서 파티클 재생
        particle.Play();

    }
}