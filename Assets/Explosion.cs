using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPause
{
    [SerializeField] int m_damage = 1;
    Animator m_anim;
    bool isMove = true;
    void Start()
    {
        m_anim = this.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if(m_anim.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && isMove)
        {
            var Player = collision.GetComponent<PlayerHealth>();
            Player.TakeDamage(m_damage);
            isMove = false;
        }
    }
    void IPause.Pause()
    {
        m_anim.speed = 0;
    }
    void IPause.Resume()
    {
        m_anim.speed = 1;
    }

}
