using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomingScript : MonoBehaviour,IPause
{
    Rigidbody2D m_rb;
    Animator m_anim;
    [SerializeField] float m_speed = 1f;
    float m_timer = 0f;
    float m_limitTime;
    [SerializeField] int m_damage = 1;
    PlayerHealth player;
    Vector2 PlayerPos;
    Vector2 Move;
    [SerializeField] bool m_flipX = false;
    [SerializeField] string m_animName = "Explosion";
    bool isMove = true;

    [SerializeField] GameObject m_explosion;
    void Start()
    {
        m_limitTime = Random.Range(5, 10);
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMove)
        {
            UpdateMove();
            UpdateTimer();
        }

        PlayerPos = GameObject.FindWithTag("Player").transform.position;

        if(m_flipX)
        {
            FlipX(Move.x);
        }
    }

    void UpdateMove()
    {
        Vector2 EnemyPos = this.transform.position;
        Move = (PlayerPos - EnemyPos);
        m_rb.velocity = new Vector2(Move.x, Move.y).normalized * m_speed;
    }

    void UpdateTimer()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_limitTime)
        {
            m_anim.Play(m_animName);
        }
    }
    void Explosion()
    {
        Instantiate(m_explosion, this.transform.position, Quaternion.identity);
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.gameObject.name == "Bom(Clone)")
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.TakeDamage(m_damage);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    void FlipX(float horizontal)
    {

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    void IPause.Pause()
    {
        m_anim.speed = 0;
        m_flipX = false;
        isMove = false;
        m_rb.Sleep();
    }
    void IPause.Resume()
    {
        m_anim.speed = 1;
        m_flipX = true;
        isMove = true;
        m_rb.WakeUp();
    }
}
