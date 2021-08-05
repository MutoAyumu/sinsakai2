using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : MonoBehaviour
{
    [SerializeField] EnemyHealth m_enemy;
    [SerializeField] GameObject[] m_children = default;
    [SerializeField] Transform instancePos = default;

    Rigidbody2D m_rb;
    Animator m_anim;

    float m_hpJudge;
    float m_timer = 0;
    bool m_attack = false;
    bool m_ins = false;
    int instance = 5;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_hpJudge = m_enemy.m_currentHp;
    }

    void Update()
    {
        AttackPhase();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
         //開始のアニメーションを流してプレイヤーの動きを止める
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //アニメーションが終わったらstartPhaseを動かす

        }
    }
    void AttackPhase()
    {       
        //体力が三分の二以上の時
        if(m_enemy.m_currentHp > (0.6f / m_hpJudge))
        {
            if (!m_attack)
            {


                for (int i = 0; i < instance; i++)
                {
                    if (m_timer > 2)
                    {
                        m_timer = 0;
                        int instanceName = Random.Range(0, m_children.Length - 1);
                        Instantiate(m_children[instanceName], instancePos);
                    }
                }
            }
        }
        //体力が三分の一以上の時
        else if (m_enemy.m_currentHp > (m_hpJudge / 0.3f))
        {

        }
        //それ以外
        else
        {

        }
    }
}
