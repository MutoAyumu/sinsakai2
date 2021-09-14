using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Switch : MonoBehaviour
{
    [SerializeField] PlayableDirector m_timeLine;
    bool isPlay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlay)
        {
            m_timeLine.Play();
            isPlay = true;
        }

    }
}
