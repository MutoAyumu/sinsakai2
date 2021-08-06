using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : MonoBehaviour
{
    [SerializeField] EnemyHealth m_enemy;
    [SerializeField] GameObject[] m_children = default;
    [SerializeField] GameObject instancePos = default;
    [SerializeField] GameObject m_originPos = default;
    [SerializeField] Vector2 m_wall = Vector2.zero;
    [SerializeField] LayerMask m_layerMask = 0;
    [SerializeField] bool m_flipX = false;
    [SerializeField] float m_speed = 3f;
    [SerializeField] float m_instanceTime = 1f;

    Rigidbody2D m_rb;
    Animator m_anim;

    float m_hpJudge;
    float m_timer = 0;
    int m_instance;
    int m_count = 0;
    bool m_firstMove = true;
    bool m_randomIns = false;


    Vector2 m_dir = Vector2.zero;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_hpJudge = m_enemy.m_currentHp;
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_enemy.m_currentHp > 0.6 * m_hpJudge)
        {
            StartAttackMove();

        }
        else if (m_enemy.m_currentHp > 0.3 * m_hpJudge)
        {

        }
        else
        {

        }

        if (m_flipX)
        {
            FlipX(m_wall.x);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //開始のアニメーションを流してプレイヤーの動きを止める
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //アニメーションが終わったらstartPhaseを動かす

        }
    }
    void StartPhase()
    {

        if (!m_randomIns)
        {
            m_instance = Random.Range(3, 5);
            m_count = 0;
            m_randomIns = true;
        }

        if (m_count < m_instance)
        {
            for (int i = 0; i < m_instance; i++)
            {
                if (m_timer > m_instanceTime)
                {
                    int instanceName = Random.Range(0, m_children.Length);
                    Instantiate(m_children[instanceName], instancePos.transform.position, Quaternion.identity);
                    m_timer = 0;
                    m_count++;
                }
            }
        }
    }

    void StartAttackMove()
    {
        Debug.DrawRay(m_originPos.transform.position, m_wall, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(m_originPos.transform.position, m_wall, m_wall.magnitude, m_layerMask);

        if (hit.collider)
        {
            m_firstMove = false;
            m_wall = new Vector2(m_wall.x * -1, m_wall.y);
            m_rb.velocity = Vector2.zero;
            StartCoroutine("FirstReset");
        }

        if (m_firstMove)
        {
            StartPhase();
            m_dir = m_wall * m_speed;
            m_dir.y = m_rb.velocity.y;
            m_rb.velocity = m_dir;
        }
    }

    IEnumerator FirstReset()
    {
        yield return new WaitForSeconds(5f);
        m_randomIns = false;
        m_firstMove = true;
    }
    void FlipX(float horizontal)
    {

        if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
