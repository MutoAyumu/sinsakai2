using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionScript : MonoBehaviour
{
    Rigidbody2D m_rb;
    Animator m_anim;
    [SerializeField] float m_speed = 1f;
    float m_timer = 0;
    float m_limitTime;
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
        UpdateTime();

        if (m_flipX)
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

    void UpdateTime()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_limitTime)
        {
            m_anim.Play("ConfusionFade");
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPos = collision.gameObject.transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
}
