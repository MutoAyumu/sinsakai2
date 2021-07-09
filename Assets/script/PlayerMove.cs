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
        m_anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        

        
    }

    void Update()
    {
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");
        
        //接地時の移動
        if (Input.GetButton("Horizontal"))
        {
            m_anim.SetBool("horizontal",true);

            if (m_h > 0)
            {
                m_rb.velocity = new Vector2(m_movepower * m_h, m_rb.velocity.y);
            }
            else
            {
                m_rb.velocity = new Vector2(m_movepower * m_h, m_rb.velocity.y);
            }
        }
        else
        {
            m_anim.SetBool("horizontal", false);
        }

        //空中時の移動
        //if(Input.GetButton("Horizontal") && air == true)
        //{
        //    if(m_h > 0)
        //    {
                
        //    }
        //    else
        //    {

        //    }
        //}

        if (m_flipX)
        {
            FlipX(m_h);
        }

        if (Input.GetButtonDown("Jump") && m_jumpcount < m_maxjump)
        {
            Debug.Log("a");
            air = true;
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumppower);
            m_jumpcount++;
        }
    }

    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            air = false;
            m_jumpcount = 0;
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
