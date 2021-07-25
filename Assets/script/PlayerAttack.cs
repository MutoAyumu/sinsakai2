using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]PlayerMove m_enemy;
    [SerializeField] int m_damage = 1;

    void MainPlayerAttack()
    {
        if(m_enemy.m_EnemyHealth != null)
        {
            m_enemy.m_EnemyHealth.TakeDamage(m_damage);
        }
    }
}
