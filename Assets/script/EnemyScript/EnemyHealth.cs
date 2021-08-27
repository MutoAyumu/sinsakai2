using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] public int m_maxHp;
    [SerializeField] float m_score = 100;
    public int m_currentHp;
    [SerializeField] Slider m_slider;
    int m_maxValue;
    Animator m_anim;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_maxValue = (int)m_slider.maxValue;
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
            DOVirtual.Float(m_slider.value, (float)m_currentHp / m_maxHp, 0.5f, value => m_slider.value = value);
            var Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
            Gmanager.Score(m_score);

            if (this.gameObject.name == "BossEnemy")
            {
                Gmanager.m_gameSet = true;
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
        if (this.gameObject.name == "BossEnemy")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }
    }
}
