using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    float m_Rh;
    float m_Rv;
    public Vector2 Wirepos;
    [SerializeField] float maxdir = 5;
    [SerializeField] LayerMask m_grappableLayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Rh = Input.GetAxisRaw("Horizontal");
        m_Rv = Input.GetAxisRaw("Vertical");
        Vector2 m_pos = this.transform.position;
        if(Input.GetButton("Fire1"))
        {
            RaycastHit2D m_hit = Physics2D.Raycast(m_pos, new Vector2(m_Rh, m_Rv), maxdir);
            Debug.DrawRay(m_pos, new Vector2(m_Rh, m_Rv).normalized, Color.blue, 1);
            if (m_hit.transform.gameObject.layer == m_grappableLayer)
            {

            }
        }
        
    }
}
