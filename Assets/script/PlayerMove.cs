using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //動き
    [SerializeField] float m_movepower = 1f;
    [SerializeField] float m_jumppower = 1f;

    //反転
    [SerializeField] bool m_flipX = false;

    //ジャンプカウント
    [SerializeField] int m_maxjump = 1;
    int m_jumpcount = 0;
    bool air = false;

    //入力判定
    float m_h;
    float m_v;
    float m_scaleX;

    Rigidbody2D m_rb = default;

    //アニメーション
    Animator m_anim = default;
    
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        

        
    }

    void Update()
    {
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal") && air == false)
        {
            if (m_h > 0)
            {
                m_rb.velocity = new Vector2(m_movepower * m_h, -1);
            }
            else
            {
                m_rb.velocity = new Vector2(m_movepower * m_h, -1);
            }
        }

        if (m_flipX)
        {
            FlipX(m_h);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("a");
            air = true;
            m_rb.AddForce(Vector2.up.normalized * m_jumppower, ForceMode2D.Impulse);
        }
    }

    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            air = false;
        }
    }

    //反転
    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
