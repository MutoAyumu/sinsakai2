using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour, IPause
{
    float m_h;
    float m_v;
    public GrappleRope m_grappleRope;
    public PlayerMove m_playermove;
    Transform m_transform;
    public Animator m_anim;
    [SerializeField] LayerMask m_grappleLayer = default;

    public Transform m_gunpoint;
    public Transform m_firepoint;

    [SerializeField] private bool m_hasMaxDistance = true;
    [SerializeField] private float m_MaxDistance = 4;

    [SerializeField] private bool m_lanchToPoint = true;
    [SerializeField] private LaunchType Launch_Type = LaunchType.Physics_Launch;
    [Range(0, 5)] [SerializeField] private float m_launchSpeed = 5;

    [SerializeField] private bool autoCongifureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 3;
    [SerializeField] float m_ropeLength = 5;
    [SerializeField] Transform m_grapplePoint = default;

    [HideInInspector] public Vector2 m_hitPoint;
    [HideInInspector] public Vector2 m_distanceVector;

    Rigidbody2D m_rb;
    float m_GravityNow;

    public  SpringJoint2D m_springJoint2D;
    PlayerMove Playermove;

    bool isGrapple = false;

    private enum LaunchType
    {
        Physics_Launch,//kokokiku
    }
    void Start()
    {
        m_grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        //Playermove = GameObject.Find("Playerbox").GetComponent<PlayerMove>();
        //m_GravityNow = m_rb.gravityScale;
    }

    void Update()
    {
        m_h = Input.GetAxisRaw("Horizontal");
        m_v = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Fire2"))
        {
            if (!isGrapple)
            {
                SetGrapplePoint();
            }
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            m_grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_grapplePoint.gameObject.SetActive(false);
            m_playermove.enabled = true;
            m_anim.SetBool("Wire", false);
            //m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //m_rb.gravityScale = m_GravityNow;
        }
    }
    void IPause.Pause()
    {
        isGrapple = true;
    }
    void IPause.Resume()
    {
        isGrapple = false;
    }

    void SetGrapplePoint()
    {
        if(Physics2D.Raycast(m_firepoint.position, new Vector2(m_h * m_playermove.m_playerDirection ,m_v).normalized, m_ropeLength, m_grappleLayer))
        {
            //m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            RaycastHit2D m_hit = Physics2D.Raycast(m_firepoint.position, new Vector2(m_h * m_playermove.m_playerDirection , m_v).normalized, m_ropeLength, m_grappleLayer);
            if((Vector2.Distance(m_hit.point, m_firepoint.position) <= m_MaxDistance) || !m_hasMaxDistance)
            {
                m_hitPoint = m_hit.point;
                m_distanceVector = m_hitPoint - (Vector2)m_gunpoint.position;
                m_grappleRope.enabled = true;
                m_grapplePoint.gameObject.SetActive(true);
                m_grapplePoint.position = m_hitPoint;
                m_anim.SetBool("Wire", true);
                m_playermove.enabled = false;
            }
        }
    }

    public void Grapple()
    {
        //m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        

        if (!m_lanchToPoint && !autoCongifureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }
        if(!m_lanchToPoint)
        {
            if(autoCongifureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }
            m_springJoint2D.connectedAnchor = m_hitPoint;
            m_springJoint2D.enabled = true;
        }

        else
        {
            if(Launch_Type == LaunchType.Physics_Launch)
            {
                m_springJoint2D.connectedAnchor = m_hitPoint;
                m_springJoint2D.distance = 0;
                m_springJoint2D.frequency = m_launchSpeed;
                m_springJoint2D.enabled = true;
                //m_rb.gravityScale = 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(m_hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(m_firepoint.position, m_MaxDistance);
        }
    }
}
