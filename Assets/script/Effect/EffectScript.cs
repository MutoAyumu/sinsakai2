using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    [SerializeField] int m_damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.GetComponent<EnemyHealth>();
            enemy.TakeDamage(m_damage);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
