using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingFloor : MonoBehaviour, IPause
{
    Rigidbody2D m_rb;
    Vector2 m_move;
    Animator m_anim;
    
    [SerializeField] Transform[] m_target;
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_stopDis = 0.1f;
    int m_count = 0;

    void Start()
    {
        m_rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        float distance = Vector2.Distance(this.transform.position, m_target[m_count].position);

        if (distance > m_stopDis)
        {
            m_move = (m_target[m_count].transform.position - this.transform.position);
            m_rb.velocity = new Vector2(m_move.x, m_move.y).normalized * m_speed;
        }
        else
        {
            m_count = (m_count + 1) % m_target.Length;
        }
    }
    void IPause.Pause()
    {
        m_rb.simulated = false;
        m_rb.Sleep();
    }
    void IPause.Resume()
    {
        m_rb.simulated = true;
        m_rb.WakeUp();
    }
}
