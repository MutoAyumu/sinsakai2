using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Patrol : MonoBehaviour
{

    Rigidbody2D m_rb;
    [SerializeField] float m_speed = 1f;

    [SerializeField] Vector2 m_rayForGround = Vector2.zero;
    [SerializeField] LayerMask m_groundLayer = 0;

    [SerializeField] Vector2 m_rayForWall = Vector2.zero;
    [SerializeField] LayerMask m_wallLayer = 0;

    Vector2 dir = Vector2.zero;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 mtf = this.transform.position;
        Debug.DrawRay(mtf, m_rayForGround, Color.green );    // ray を Scene 上に描く
        Debug.DrawRay(mtf, m_rayForWall, Color.green);
        RaycastHit2D hitGround = Physics2D.Raycast(this.transform.position, m_rayForGround, m_rayForGround.magnitude, m_groundLayer);
        RaycastHit2D hitWall = Physics2D.Raycast(this.transform.position, m_rayForWall, m_rayForWall.magnitude, m_wallLayer);


        if (!hitGround.collider || hitWall.collider)
        {
            m_rayForGround = new Vector2(m_rayForGround.x * -1, m_rayForGround.y);
            m_rayForWall = new Vector2(m_rayForWall.x * -1, m_rayForWall.y);
        }

        dir = m_rayForGround * m_speed;
        dir.y = m_rb.gravityScale;
        m_rb.velocity = dir;
    }
}
