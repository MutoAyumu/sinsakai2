using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyMove m_hit;
    [SerializeField] int m_damage = 1;


    void NormalEnemyAttack()
    {

        if (m_hit.m_player != null)
        {
            m_hit.m_player.TakeDamage(m_damage);
        }
    }


}
