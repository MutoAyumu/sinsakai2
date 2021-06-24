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
    //動き
    [SerializeField] float m_movePower = 5f;
    [SerializeField] float m_jumpPower = 5f;
    //反転
    [SerializeField] bool m_flipX = false;
    [SerializeField] bool m_flipY = false;
    //ジャンプのカウント
    int jumpcount = 0;
    [SerializeField] int max_jumpcount = 1;

    float m_h;
    float m_v;
    float m_scaleX;
    float m_scaleY;
    Rigidbody2D m_rb = default;
    //float new_gravity = 0;
    //float gravity;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();


        //gravity = m_rb.gravityScale; 
    }

    // Update is called once per frame
    void Update()
    {
        //入力を受ける
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        if (m_flipX)
        {
            FlipX(m_h);
        }

        if (m_flipY)
        {
            FlipY(m_v);
        }

        //my transform
        Vector2 mtf = this.transform.position;

        //攻撃の入力
        if (Input.GetButtonDown("Fire1"))
        {

            if (!Input.GetButton("Vertical") && Input.GetButton("Horizontal"))
            {
                //一瞬だけ座標を固定する
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                //m_rb.gravityScale = new_gravity;

                if (m_scaleX > 0)
                {
                    Instantiate(m_attackh, new Vector2(mtf.x + 2, mtf.y), Quaternion.Euler(0,0,180));
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_attackh, new Vector2(mtf.x - 2, mtf.y), Quaternion.Euler(0,0,0));
                }
                //固定を解除
                m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //m_rb.gravityScale = gravity;
            }

            if (!Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
            {
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (m_scaleY > 0)
                {
                    Instantiate(m_attackv, new Vector2(mtf.x, mtf.y + 2), Quaternion.Euler(0,0,90));
                }
                else if (m_scaleY < 0)
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
                //一瞬だけ座標を固定する
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                //m_rb.gravityScale = new_gravity;

                if (m_scaleX > 0)
                {
                    Instantiate(m_skill, new Vector2(mtf.x + 2, mtf.y), this.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_skill, new Vector2(mtf.x - 2, mtf.y), this.transform.rotation);
                }
                //固定を解除
                m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //m_rb.gravityScale = gravity;
            }

            if (!Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
            {
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (m_scaleY > 0)
                {
                    Instantiate(m_skill, new Vector2(mtf.x, mtf.y + 2), this.transform.rotation);
                }
                else if (m_scaleY < 0)
                {
                    Instantiate(m_skill, new Vector2(mtf.x, mtf.y - 2), this.transform.rotation);
                }
                m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        //ジャンプの入力
        if (Input.GetButtonDown("Jump") && jumpcount < max_jumpcount)
        {
            m_rb.AddForce(Vector2.up.normalized * m_jumpPower, ForceMode2D.Impulse);
            jumpcount++;
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

    private void FixedUpdate()
    {
        m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);

    }
    //水平の反転
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

    //垂直の反転
    void FlipY(float vertical)
    {
        m_scaleY = this.transform.localScale.y;

        if (vertical > 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, Mathf.Abs(this.transform.localScale.y), this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, -1 * Mathf.Abs(this.transform.localScale.y), this.transform.localScale.z);
        }
    }

    
}   

