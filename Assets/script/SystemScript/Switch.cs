using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Switch : MonoBehaviour
{
    [SerializeField] UnityEvent m_event = default;
    bool isPlay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlay && collision.gameObject.tag == "Player")
        {
            //m_timeLine.Play();
            m_event.Invoke();
            isPlay = true;
        }
    }
}
