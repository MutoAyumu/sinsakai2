using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour, IPause
{
    [SerializeField] float m_destroyTime = 5f;
    float m_timer;
    Rigidbody2D[] m_rb;
    bool isActive = true;

    private void Start()
    {
        m_rb = GetComponentsInChildren<Rigidbody2D>();
        m_timer = m_destroyTime;
    }
    private void Update()
    {
        if (isActive)
        {
            m_timer -= Time.deltaTime;

            if (m_timer < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void IPause.Pause()
    {
        for (int a = 0; a < m_rb.Length; a++)
            m_rb[a].simulated = false;
        isActive = false;
    }
    void IPause.Resume()
    {
        for (int a = 0; a < m_rb.Length; a++)
            m_rb[a].simulated = true;
        isActive = true;
    }
}
