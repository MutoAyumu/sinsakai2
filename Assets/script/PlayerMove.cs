using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //動き
    [SerializeField] float m_movepower = 1f;
    [SerializeField] float m_jumppower = 1f;
    [SerializeField] float m_dashSpeed = 2f;

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
    float m_drag;

    float m_timer = 0f;
    [SerializeField] float m_setTime = 3f;

    //アニメーション
    Animator m_anim = default;

    [HideInInspector]public EnemyHealth m_EnemyHealth;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        //m_drag = m_rb.drag;
    }

    private void FixedUpdate()//決まった一定のタイミングで繰り返される
    {

        //接地時の移動
        
        if (air == false)//
        {
            m_anim.SetBool("horizontal", true);
        }

        if (air != false)
        {
            m_anim.SetBool("horizontal", false);
        }

        m_rb.velocity = new Vector2(m_movepower * m_h, m_rb.velocity.y);
        
        if(m_h == 0)
        {
            m_anim.SetBool("horizontal", false);
        }
    }

    void Update()
    {
        //入力判定
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        if (m_flipX)
        {
            FlipX(m_h);
        }

        if (Input.GetButtonDown("Jump") && m_jumpcount < m_maxjump && air == false)//ボタンが押される・カウントが小さいとき・地面についているとき
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumppower);
            m_jumpcount++;
        }

        m_timer += Time.deltaTime;

        //攻撃
        if (Input.GetButtonDown("Fire1") && m_timer >= m_setTime)
        {
            m_anim.SetBool("NormalAttack", true);
            m_timer = 0;
        }
    }

    private void AttackFin()
    {
        m_anim.SetBool("NormalAttack", false);
    }

    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            m_jumpcount = 0;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            air = true;
            //m_rb.drag = 0;
            if (m_anim.GetBool("Wire") == false)
            {
                m_anim.SetBool("Jump", true);
            }//ジャンプアニメーションに遷移
        }

        if(collision.gameObject.tag == "Enemy")
        {
            m_EnemyHealth = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            m_EnemyHealth = collision.GetComponent<EnemyHealth>();
        }

        if (collision.gameObject.tag == "ground")
        {

            //m_rb.drag = m_drag;
            
            if (m_anim.GetBool("Wire") == false)
            {
                air = false;
                m_anim.SetBool("Jump", false);//待機アニメーションに遷移
            }
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
