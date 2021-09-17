using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMoving : MonoBehaviour
{
    [SerializeField] UnityEvent m_playerJoinEvent = default;
    [SerializeField] UnityEvent m_playerExitEvent = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_playerJoinEvent.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        m_playerExitEvent.Invoke();
    }
}
