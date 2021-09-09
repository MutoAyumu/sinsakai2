using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyMove : MonoBehaviour, IPause
{
    Vector2 m_enemyPos;
    Vector2 m_playerPos;
    Vector2 m_move;
    Rigidbody2D m_rb;
    Animator m_anim;
    float m_scaleX;

    [SerializeField] Transform[] m_target;
    [SerializeField] float m_stopDis = 0.1f;
    int n = 0;
    bool m_patrol = false;

    [SerializeField] float m_attackDis = 0.5f;
    [SerializeField]float m_speed = 1f;
    [SerializeField] bool m_flipX = false;

    bool isMove = false;

    public PlayerHealth m_player;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!m_patrol && !isMove)
        {
            Patrol();
        }

        if(m_flipX)
        {
            FlipX(m_move.x);
        }
    }

    void Tracking(PlayerHealth player)
    {
        float distance = Vector2.Distance(transform.position, m_playerPos);

        if (distance > m_attackDis)
        {
            m_enemyPos = transform.position;
            m_move = (m_playerPos - m_enemyPos);
            m_rb.velocity = new Vector2(m_move.x, m_move.y).normalized * m_speed;
            m_anim.SetBool("Attack", false);
            m_player = null;
        }
        else
        {
            m_anim.SetBool("Attack", true);
            m_rb.velocity = Vector2.zero;
            m_player = player;
        }
    }

    void Patrol()
    {
        float distance = Vector2.Distance(this.transform.position, m_target[n].position);

        if(distance > m_stopDis)
        {
            m_move = (m_target[n].transform.position - this.transform.position);
            m_rb.velocity = new Vector2(m_move.x, m_move.y).normalized * m_speed;
            m_anim.SetBool("Attack", false);
        }
        else
        {
            n = (n + 1) % m_target.Length;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" && !isMove)
        {
            m_playerPos = collider.transform.position;
            Tracking(collider.GetComponent<PlayerHealth>());
            m_patrol = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !isMove)
        {
            
            m_patrol = false;
        }
    }
    
    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    void IPause.Pause()
    {
        m_rb.Sleep();
        m_anim.speed = 0;
        m_flipX = false;
        isMove = true;
    }
    void IPause.Resume()
    {
        m_rb.WakeUp();
        m_anim.speed = 1;
        m_flipX = true;
        isMove = false;
    }
}
