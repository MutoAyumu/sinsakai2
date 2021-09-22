using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakEffect : MonoBehaviour, IPause
{
    ParticleSystem m_particle;
    void Start()
    {
        m_particle = GetComponent<ParticleSystem>();
    }

    void IPause.Pause()
    {
        m_particle.Pause();
    }
    void IPause.Resume()
    {
        m_particle.Play();
    }
}
