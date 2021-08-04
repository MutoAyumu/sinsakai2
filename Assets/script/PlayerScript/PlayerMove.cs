using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    //動き
    [SerializeField] public float m_movepower = 1f;
    [SerializeField] public float m_jumppower = 1f;
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
    float m_timer = 0f;
    [SerializeField] float m_setTime = 3f;

    //アニメーション
    Animator m_anim = default;
    [HideInInspector] public EnemyHealth m_EnemyHealth;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }
    void Update()
    {
        //Time.timeScaleが0ならばUpdateの処理を行わない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

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
        if(inputAttack)
        {
            //経過時間が設定した時間よりも大きかったら
            if (m_timer > m_setTime)
            {
                m_anim.SetBool("NormalAttack", true);
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
        m_rb.velocity = new Vector2(m_movepower * m_h, m_rb.velocity.y);
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
        m_anim.SetBool("NormalAttack", false);
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
        //入ったコライダーのTagが"Enemy"なら
        if (collision.gameObject.tag == "Enemy")
        {
            m_EnemyHealth = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //入ったコライダーのTagが"Enemy"なら
        if (collision.gameObject.tag == "Enemy")
        {
            //そのコライダーにアタッチされたEnemyHealthコンポーネントを取得
            m_EnemyHealth = collision.GetComponent<EnemyHealth>();
        }
    }
    /// <summary>反転</summary>
    /// <param name="horizontal"></param>
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
