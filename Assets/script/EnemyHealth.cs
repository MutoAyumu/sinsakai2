using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] public int max_hp = 1;
    public int currenHp;
    int maxvalue;

    [SerializeField] public int _damage = 1;

    public Slider slider;

    public Transform m_target;
    [SerializeField] float speed;
    [SerializeField] GameObject tgp;
    bool join = false;
    Rigidbody2D m_rb;

    private void Start()
    {
        maxvalue = (int)slider.maxValue;
        slider.value = maxvalue;
        currenHp = max_hp;

        m_rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(_damage);
        }
    }


    public void TakeDamage(int damage)
    {
        currenHp -= damage;
        slider.value -= damage;

        if (currenHp <= 0)
        {
            currenHp = 0;

            Destroy(gameObject);
        }
    }

    void Tracking()
    {
        Vector2 playerPos = m_target.position;
        Vector2 enemyPos = transform.position;
        Vector2 force = (playerPos - enemyPos) * speed;
        m_rb.AddForce(force, ForceMode2D.Force);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            join = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        join = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (join == true)
        {
            Tracking();
        }
    }
}
