using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    //動き
    [SerializeField] public float m_movepower = 1f;
    [SerializeField] public float m_jumppower = 1f;
    [HideInInspector] public float m_playerDirection = 1f;
    //反転
    [SerializeField] bool m_flipX = false;

    //ジャンプカウント
    bool isAir;
    bool isJumping;
    float chargedJumpInput;
    [SerializeField] float jumpChargeLimit = 0.5f;

    //入力判定
    float m_h;
    float m_v;
    float m_scaleX;
    Rigidbody2D m_rb = default;
    PlayerHealth m_player = default;
    float m_timer = 0f;
    [SerializeField] float m_setTime = 3f;

    //攻撃のRay
    [SerializeField] GameObject m_originPos = default;
    [SerializeField]LayerMask m_layer = default;
    [SerializeField] Vector2 m_ray = Vector2.zero;
    RaycastHit2D m_hit;

    //攻撃時のEffectinstance
    [SerializeField] GameObject m_rayHitObject = default;
    [SerializeField] GameObject m_rayObject = default;
    [SerializeField] GameObject m_rayUpHitObject = default;
    [SerializeField] GameObject m_rayUpObject = default;

    //アニメーション
    Animator m_anim = default;
    [HideInInspector] public EnemyHealth m_EnemyHealth;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_player = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        //Time.timeScaleが0ならばUpdateの処理を行わない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        Debug.DrawRay(m_originPos.transform.position, new Vector2(m_ray.x * this.transform.localScale.x, 0), Color.green);
        Debug.DrawRay(m_originPos.transform.position, new Vector2(0, m_ray.y), Color.green);

        InputMove();
        UpdateMove();
        InputJump();
        UpdateJump();
        InputAttack();
    }
    /// <summary>攻撃の入力</summary>
    void InputAttack()
    {
        m_timer += Time.deltaTime;
        var inputAttack = Input.GetButtonDown("Fire1");
        //ボタンが押されていたら
        if (inputAttack)
        {
            //経過時間が設定した時間よりも大きかったら
            if (m_timer > m_setTime)
            {

                if (m_v > 0.5)
                {
                    m_hit = Physics2D.Raycast(m_originPos.transform.position, new Vector2(0, m_ray.y), m_ray.magnitude, m_layer);
                }
                else
                {
                    //ray出す
                    m_hit = Physics2D.Raycast(m_originPos.transform.position, new Vector2(m_ray.x * this.transform.localScale.x, 0), m_ray.magnitude, m_layer);
                }

                //当たったら
                if(m_hit && m_v > 0.5)
                {
                    m_EnemyHealth = m_hit.collider.GetComponent<EnemyHealth>();
                    m_anim.SetBool("Attack", true);
                    m_anim.SetBool("VarticalAttack", true);
                    Instantiate(m_rayUpHitObject, m_hit.point, Quaternion.identity);
                }
                else if(!m_hit && m_v > 0.5)
                {
                    m_EnemyHealth = null;
                    m_anim.SetBool("VarticalAttack", true);
                    m_anim.SetBool("Attack", true);
                    Instantiate(m_rayUpObject, new Vector2(m_originPos.transform.position.x, m_originPos.transform.position.y + m_ray.y), Quaternion.identity);
                }
                else if(m_hit)
                {
                    m_EnemyHealth = m_hit.collider.GetComponent<EnemyHealth>();
                    m_anim.SetBool("HorizontalAttack", true);
                    m_anim.SetBool("Attack", true);
                    Instantiate(m_rayHitObject, m_hit.point, Quaternion.identity);
                }
                else
                {
                    m_EnemyHealth = null;
                    m_anim.SetBool("HorizontalAttack", true);
                    m_anim.SetBool("Attack", true);
                    Instantiate(m_rayObject, new Vector2(m_originPos.transform.position.x + m_ray.x * this.transform.localScale.x, m_originPos.transform.position.y), Quaternion.identity);
                }

                m_timer = 0;
            }
        }
    }
    /// <summary>移動の入力関数</summary>
    void InputMove()
    {
        //入力
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        //反転
        if (m_flipX)
        {
            FlipX(m_h);
        }
    }
    /// <summary>移動の動きの部分</summary>
    void UpdateMove()
    {
        //移動
        m_rb.velocity = new Vector2(m_movepower * m_h * m_playerDirection, m_rb.velocity.y);
        //ジャンプ中じゃなければ
        if (!isAir)
        {
            m_anim.SetBool("horizontal", true);
        }
        //ジャンプ中なら
        if (isAir)
        {
            m_anim.SetBool("horizontal", false);
        }
        //横入力がされていなければ
        if (m_h == 0)
        {
            m_anim.SetBool("horizontal", false);
        }
    }
    /// <summary>ジャンプの入力</summary>
    void InputJump()
    {
        // インプット
        var inputJump = Input.GetButton("Jump");
        //入力があれば
        if (inputJump)
        {
            // 空中じゃないなら
            if (!isAir)
            {
                isJumping = true;
            }
            // じゃんぷ中なら
            if (isJumping)
            {
                chargedJumpInput += Time.deltaTime;
            }
        }
        else
        {
            chargedJumpInput = jumpChargeLimit;
        }
        //ジャンプ中でなく空中でもなければ
        if (!isAir && !inputJump)
        {
            chargedJumpInput = 0f;
            isJumping = false;
        }
    }
    /// <summary>チャージジャンプの動き</summary>
    void UpdateJump()
    {
        //Debug.Log($"input:{chargedJumpInput}");
        // じゃんぷ中かつリミットに達していなかったら
        if (isJumping && chargedJumpInput < jumpChargeLimit)
        {
            m_anim.SetBool("Jump", true);
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumppower);
            //m_rb.velocity += Vector2.up * m_jumppower * Time.deltaTime;
        }
        else
        {
            m_anim.SetBool("Jump", false);
        }
    }
    private void AttackFin()
    {
        m_anim.SetBool("Attack", false);

        if (m_anim.GetBool("HorizontalAttack"))
        {
            m_anim.SetBool("HorizontalAttack", false);
        }
        if(m_anim.GetBool("VarticalAttack"))
        {
            m_anim.SetBool("VarticalAttack", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //入ったコライダーのTagが"ground"なら
        if (collision.gameObject.tag == "ground")
        {
            isAir = false;
            m_anim.SetBool("Down", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //入ったコライダーのTagが"ground"なら
        if (collision.gameObject.tag == "ground")
        {
            isAir = true;
            m_anim.SetBool("Down", true);
        }
    }
    /// <summary>反転</summary>
    /// <param name="horizontal"></param>
    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;
        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(m_playerDirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            m_rayObject.transform.localScale = new Vector3(m_playerDirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            m_rayHitObject.transform.localScale = new Vector3(m_playerDirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * m_playerDirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            m_rayObject.transform.localScale = new Vector3(-1 * m_playerDirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            m_rayHitObject.transform.localScale = new Vector3(-1 * m_playerDirection * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
