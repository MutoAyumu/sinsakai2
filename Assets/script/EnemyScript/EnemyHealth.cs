using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] public int m_maxHp;
    [SerializeField] int m_score = 100;
    public int m_currentHp;
    [SerializeField] Slider m_slider;
    int m_maxValue;
    Animator m_anim;
    private void Start()
    {
        m_anim = GetComponent<Animator>();
        if (m_slider)
        {
            m_maxValue = (int)m_slider.maxValue;
            m_slider.value = 1f;
        }
        m_currentHp = m_maxHp;
    }

    public void TakeDamage(int damage)
    {
        
        m_currentHp -= damage;
        
        if(m_currentHp > 0)
        {
            if(m_slider)
            DOVirtual.Float(m_slider.value, (float)m_currentHp / m_maxHp, 0.5f, value => m_slider.value = value);
        }
        else
        {
            if(m_slider)
            DOVirtual.Float(m_slider.value, (float)m_currentHp / m_maxHp, 0.5f, value => m_slider.value = value);
            var Smanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            Smanager.Score(m_score);

            if (this.gameObject.name == "BossEnemy")
            {
                Smanager.m_gameSet = true;
                m_anim.Play("EndAnimation");
            }
            else
            {
                Destroy();
            }
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
