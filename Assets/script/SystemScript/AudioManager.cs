using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instanceAudio = null;
    public float m_volume;
    private void Awake()
    {
        if (instanceAudio != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            AudioListener.volume = 1;
            m_volume = AudioListener.volume;
            instanceAudio = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
