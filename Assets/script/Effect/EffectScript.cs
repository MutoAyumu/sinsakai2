using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour, IPause
{
    AudioSource m_audio;
    Animator m_anim;
    
    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_anim = GetComponent<Animator>();
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void IPause.Pause()
    {
        m_anim.speed = 0;
        m_audio.Pause();
    }
    void IPause.Resume()
    {
        m_anim.speed = 1;
        m_audio.UnPause();
    }
}
