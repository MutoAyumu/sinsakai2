using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour, IPause
{
    [SerializeField] GameObject m_object = default;
    [SerializeField]float m_generatorTime = 3f;
    float m_timer;
    [SerializeField] float m_randomMin = 1f;
    [SerializeField] float m_randomMax = 2f;
    bool isActive = true;

    private void Start()
    {
        m_timer = m_generatorTime;
    }
    private void Update()
    {
        if (isActive)
        {
            Generator();
        }
    }

    void Generator()
    {
        m_timer -= Time.deltaTime;

        if(m_timer < 0)
        {
            float scale = Random.Range(m_randomMin, m_randomMax);
            m_object.transform.localScale = new Vector2(scale, scale);
            Instantiate(m_object, this.transform.position, Quaternion.identity,this.gameObject.transform);
            m_timer = m_generatorTime;
        }
    }
    void IPause.Pause()
    {
        isActive = false;
    }
    void IPause.Resume()
    {
        isActive = true;
    }
}
