using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //水平・垂直の攻撃
    [SerializeField] GameObject m_attackh = default;
    [SerializeField] GameObject m_attackv = default;
    //ワイヤー
    [SerializeField] GameObject m_skill = default;
    //プレイヤーの動き
    [SerializeField] float m_movePower = 5f;
    [SerializeField] float m_jumpPower = 5f;
    //反転
    [SerializeField] bool m_flipX = false;
    //ジャンプのカウント
    int jumpcount = 0;
    [SerializeField] int max_jumpcount = 1;
    //攻撃位置の指定
    [SerializeField] Transform m_muzzle = null;

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

    void Update()
    {
        //移動
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");
        //m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
        Vector2 dir = new Vector2(m_h, -1).normalized;
        m_rb.velocity = dir * m_movePower;

        if (m_flipX)
        {
            FlipX(m_h);
        }

        //ジャンプ
        if (Input.GetButtonDown("Jump") && jumpcount < max_jumpcount)
        {
            //m_rb.AddForce(Vector2.up.normalized * m_jumpPower, ForceMode2D.Impulse);
            Vector2 dir1 = new Vector2(m_h, 10).normalized;
            m_rb.velocity = dir1 * m_movePower;
            jumpcount++;
        }

        //My transform
        Vector2 mtf = this.transform.position;

        //攻撃の入力
        if (Input.GetButtonDown("Fire1"))
        {

            if (!Input.GetButton("Vertical") && Input.GetButton("Horizontal"))
            {
                //一瞬だけ座標を固定する
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (m_scaleX > 0)
                {
                    //Instantiate(m_attackh, new Vector2(mtf.x + 2, mtf.y), Quaternion.Euler(0,0,180));
                    Instantiate(m_attackh, m_muzzle.position, m_attackh.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_attackh, new Vector2(mtf.x - 2, mtf.y), Quaternion.Euler(0,0,0));
                }
                //固定を解除
                m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (!Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
            {
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (jumpcount == 0)
                {
                    Instantiate(m_attackv, new Vector2(mtf.x, mtf.y + 2), Quaternion.Euler(0,0,90));
                }
                else if (jumpcount != 0)
                {
                    Instantiate(m_attackv, new Vector2(mtf.x, mtf.y - 2), this.transform.rotation);
                }
                m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
           
        }

        //ワイヤーアクション
        if(Input.GetButtonDown("Fire2"))
        {
            if (!Input.GetButton("Vertical") && Input.GetButton("Horizontal"))
            {
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (m_scaleX > 0)
                {
                    Instantiate(m_skill, new Vector2(mtf.x + 2, mtf.y), this.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_skill, new Vector2(mtf.x - 2, mtf.y), this.transform.rotation);
                }
                m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            //if (!Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
            //{
            //    m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            //    if (m_scaleY > 0)
            //    {
            //        Instantiate(m_skill, new Vector2(mtf.x, mtf.y + 2), this.transform.rotation);
            //    }
            //    else if (m_scaleY < 0)
            //    {
            //        Instantiate(m_skill, new Vector2(mtf.x, mtf.y - 2), this.transform.rotation);
            //    }
            //    m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //}
        }  
    }
    //接地判定
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jumpcount = 0;
        }
    }

    //横方向の反転
    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;

        if(horizontal > 0)
        {
            
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if(horizontal < 0)
        {
            
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform .localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}   

