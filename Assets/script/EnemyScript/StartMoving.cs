using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMoving : MonoBehaviour, IPause
{
    [SerializeField] UnityEvent m_playerJoinEvent = default;
    [SerializeField] GameObject m_enemys = default;
    bool isOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_playerJoinEvent.Invoke();
            if (m_enemys)
            {
                m_enemys.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isOn)
        {
            if (m_enemys)
            {
                m_enemys.SetActive(false);
            }
        }
    }
    void IPause.Pause()
    {
        isOn = true;
    }
    void IPause.Resume()
    {
        isOn = false;
    }
}
