using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomScript : MonoBehaviour
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
    void Start()
    {
        m_limitTime = Random.Range(5, 10);
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateMove();
        UpdateTimer();

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
            m_anim.Play("Explosion");
        }
    }
    void Explosion()
    {
        if(player != null)
        {
            player.TakeDamage(m_damage);
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.GetComponent<PlayerHealth>();
            PlayerPos = collision.gameObject.transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(m_damage);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = null;
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
}
