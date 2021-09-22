using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : MonoBehaviour, IPause
{
    [SerializeField] EnemyHealth m_enemy;
    [SerializeField] GameObject[] m_children = default;
    [SerializeField] GameObject instancePos = default;
    [SerializeField] GameObject m_originPos = default;
    [SerializeField] Vector2 m_wall = Vector2.zero;
    [SerializeField] LayerMask m_layerMask = 0;
    [SerializeField] bool m_flipX = false;
    [SerializeField] float m_speed = 3f;
    [SerializeField] float m_secondSpeed = 10f;
    [SerializeField] float m_instanceTime = 1f;
    [SerializeField] int m_damage = 1;
    [SerializeField] GameObject m_movePoint = default;
    
    Vector2 m_playerPos;
    Rigidbody2D m_rb;
    Animator m_anim;
    Vector2 m_enemyPos = default;
    GameManager m_Gmanager;

    float m_angularVelocity;
    Vector2 m_velocity;

    float m_hpJudge;
    float m_timer = 0;
    float m_secondTimer = 0;
    int m_instance;
    int m_count = 0;
    bool m_firstMove = true;
    bool m_randomIns = false;
    bool m_secondMove = false;
    bool m_phaseChange = true;
    bool m_gameStart = false;
    bool m_gameEnd = false;
    bool isMove = true;


    Vector2 m_dir = Vector2.zero;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_hpJudge = m_enemy.m_maxHp;
        m_rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        m_Gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {

        if (m_gameStart && !m_gameEnd && isMove && !m_Gmanager.isOver)
        {
            if (m_enemy.m_currentHp > 0.5 * m_hpJudge)
            {
                FirstPhase();
            }
            else
            {
                SecondPhase();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameStart();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            var Player = collision.gameObject.GetComponent<PlayerHealth>();
            Player.TakeDamage(m_damage);
        }
    }

    void FirstAction()
    {
        m_timer += Time.deltaTime;

        if (!m_randomIns)
        {
            m_instance = Random.Range(3, 6);
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

    void FirstPhase()
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
            FirstAction();
            m_dir = m_wall * m_speed;
            m_dir.y = m_rb.velocity.y;
            m_rb.velocity = m_dir;
        }

        if (m_flipX)
        {
            FlipX(m_wall.x);
        }
    }

    IEnumerator FirstReset()
    {
        yield return new WaitForSeconds(5f);
        m_randomIns = false;
        m_firstMove = true;
    }

    void PhaseChange()
    {
        m_rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        float dis = Vector2.Distance(m_movePoint.transform.position, this.transform.position);
        Vector2 move = m_movePoint.transform.position - this.transform.position;

        if (dis > 0.1f)
        {
            float speed = 5;            
            m_rb.velocity = move.normalized * speed;
        }
        else
        {
            m_rb.velocity = Vector2.zero;
            m_phaseChange = false;
            StartCoroutine("ChangeMove");
        }

        if (m_flipX)
        {
            FlipX(move.x);
        }
    }

    IEnumerator ChangeMove()
    {
        yield return new WaitForSeconds(3);
        m_secondMove = true;
    }

    void SecondPhase()
    {
        
        if (m_phaseChange)
        {
            PhaseChange();
        }

        else if (m_secondMove)
        {
            SecondAction();
        }
    }

    void SecondAction()
    {
        m_enemyPos = this.transform.position;
        m_playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 m_move = (m_playerPos - m_enemyPos).normalized;

        m_secondTimer += Time.deltaTime;

        if(m_secondTimer < 10)
        {
            m_rb.velocity += m_move * m_secondSpeed * Time.deltaTime;
        }
        else
        {
            m_rb.velocity = Vector2.zero;
            StartCoroutine("SecondReset");
        }

        if (m_flipX)
        {
            FlipX(m_move.x);
        }
    }

    IEnumerator SecondReset()
    {
        yield return new WaitForSeconds(5);
        m_secondTimer = 0;
    }

    void GameStart()
    {
        m_gameStart = true;
    }

    void GameEnd()
    {
        m_gameEnd = true;
        m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
    void IPause.Pause()
    {
        m_flipX = false;
        m_anim.speed = 0;
        isMove = false;
        m_angularVelocity = m_rb.angularVelocity;
        m_velocity = m_rb.velocity;
        m_rb.simulated = false;
        m_rb.Sleep();
    }
    void IPause.Resume()
    {
        m_flipX = true;
        m_anim.speed = 1;
        isMove = true;
        m_rb.simulated = true;
        m_rb.angularVelocity = m_angularVelocity;
        m_rb.velocity = m_velocity;
        m_rb.WakeUp();
    }
}
