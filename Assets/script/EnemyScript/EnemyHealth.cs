using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int m_maxHp;
    int m_currentHp;
    [SerializeField] Slider m_slider;
    int m_maxValue;

    private void Start()
    {
        m_maxValue = (int)m_slider.maxValue;
        m_currentHp = m_maxHp;
        m_slider.value = 1f;
        m_currentHp = m_maxHp;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        var hit = collision.gameObject;
    //        var hp = hit.GetComponent<PlayerHealth>();

    //        if (hp != null)
    //        {
    //            hp.TakeDamage(m_damage);
    //        }
    //    }
    //}
    public void TakeDamage(int damage)
    {
        m_currentHp -= damage;
        
        if(m_currentHp > 0)
        {
            DOVirtual.Float(m_slider.value, (float)m_currentHp / m_maxHp, 0.5f, value => m_slider.value = value);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
