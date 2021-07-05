using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hool : MonoBehaviour
{
    private RaycastHit hit;
    private Rigidbody2D m_rb;
    public float momentum;
    public float speed;
    public float step;
    public bool attached = false;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Physics2D.Raycast(transform.position, transform.right, 5);
            {
                attached = true;
                m_rb.isKinematic = true;
            }
        }

        if(Input.GetButtonUp("Fire2"))
        {
            attached = false;
            m_rb.isKinematic = false;
            m_rb.velocity = transform.right * momentum;
        }

        if(attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }

        //if(!attached && momentum)
        //{
        //    mo
        //}
    }
}
