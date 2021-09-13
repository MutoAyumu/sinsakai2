using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] Slider m_volume = default;
    AudioManager m_audioManager;

    private void Start()
    {
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_volume.value = m_audioManager.m_volume;
    }

    private void Update()
    {
        AudioListener.volume = m_volume.value;
        m_audioManager.m_volume = m_volume.value;
    }
}
