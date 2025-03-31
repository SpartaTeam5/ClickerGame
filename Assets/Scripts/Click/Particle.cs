using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] ParticleSystem normalParticle; // �Ϲ� ���� 
    [SerializeField] ParticleSystem criticalEffect; // ġ��Ÿ

    void Start()
    {

        if (normalParticle == null || criticalEffect == null)
        {
            Debug.LogError("��ƼŬ �ý����� �������� ����. �ν����Ϳ��� Ȯ��");
        }
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        ClickAttack clickAttack = gameObject.GetComponent<ClickAttack>();

    //        if (clickAttack != null)
    //        {
    //            bool isCritical = clickAttack.IsCriticalHit();
    //            PlayParticleSystem(isCritical);
    //        }
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        StopParticleSystem();
    //    }
    //}
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
        //void StopParticleSystem()
        //{
        //if (normalParticle != null) normalParticle.Stop();
        //if (criticalEffect != null) criticalEffect.Stop();
        //}

        void SpawnParticleMousePosition(ParticleSystem particle)
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition.z = Camera.main.nearClipPlane + 1f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            particle.transform.position = worldPosition;

            particle.Play();
        }
    
}