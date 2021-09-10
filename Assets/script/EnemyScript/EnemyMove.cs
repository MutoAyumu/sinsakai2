using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour, IPause
{
    Rigidbody2D m_rb;
    Animator m_anim;
    [HideInInspector]public AudioSource m_audio;
    Vector2 m_dir = Vector2.zero;
    [HideInInspector]public PlayerHealth m_player;

    [SerializeField] float m_speed = 1f;

    [SerializeField] bool m_flipX = false;
    float m_scaleX;

    bool m_join = true;
    bool isAttack = true;
    public bool m_moveAnim = default;

    [SerializeField] Vector2 m_rayForGround = Vector2.zero;
    [SerializeField] LayerMask m_groundLayer = 0;

    [SerializeField] Vector2 m_rayForWall = Vector2.zero;
    [SerializeField] LayerMask m_wallLayer = 0;

    [SerializeField] GameObject m_setPos;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (m_join)
        {
            Patrol();
        }

        if (m_flipX)
        {
            FlipX(m_dir.x);
        }
    }
    void Patrol()
    {
        //Vector2 mtf = this.transform.position;
        Debug.DrawRay(m_setPos.transform.position, m_rayForGround, Color.green);
        Debug.DrawRay(m_setPos.transform.position, m_rayForWall, Color.green);
        RaycastHit2D hitGround = Physics2D.Raycast(m_setPos.transform.position, m_rayForGround, m_rayForGround.magnitude, m_groundLayer);
        RaycastHit2D hitWall = Physics2D.Raycast(m_setPos.transform.position, m_rayForWall, m_rayForWall.magnitude, m_wallLayer);

        if(!hitGround.collider || hitWall.collider)
        {
            m_rayForGround = new Vector2(m_rayForGround.x * -1, m_rayForGround.y);
            m_rayForWall = new Vector2(m_rayForWall.x * -1, m_rayForWall.y);
        }
        m_dir = m_rayForGround * m_speed;
        m_dir.y = m_rb.velocity.y;
        m_rb.velocity = m_dir;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_player = collision.GetComponent<PlayerHealth>();
            m_join = false;
            m_rb.velocity = Vector2.zero;
            m_anim.SetBool("Attack", true);
            m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && isAttack)
        {
            m_player = null;
            m_anim.SetBool("Attack", false);
            StartCoroutine(StopTime());
            m_rb.constraints = RigidbodyConstraints2D.None;
            m_rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.35f);
        m_join = true;
    }

    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;

        if(horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if(horizontal < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    void IPause.Pause()
    {
        m_rb.Sleep();
        m_anim.speed = 0;
        m_flipX = false;
        m_join = false;
        isAttack = false;
        m_audio.Pause();
        StopCoroutine(StopTime());
    }
    void IPause.Resume()
    {
        m_rb.WakeUp();
        m_anim.speed = 1;
        m_flipX = true;
        m_join = true;
        isAttack = true;
        m_audio.UnPause();
    }
}
