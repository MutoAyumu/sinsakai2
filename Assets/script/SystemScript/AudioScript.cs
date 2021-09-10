using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour, IPause
{
    AudioSource m_audio;
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }
    void IPause.Pause()
    {
        m_audio.Pause();
    }
    void IPause.Resume()
    {
        m_audio.UnPause();
    }
}
