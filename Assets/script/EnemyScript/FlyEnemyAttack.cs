using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyAttack : MonoBehaviour
{
    [SerializeField] FlyEnemyMove m_hit;
    [SerializeField, HideInInspector] public int m_damage = 1;


    void flyEnemyAttack()
    {

        if (m_hit.m_player != null)
        {
            m_hit.m_player.TakeDamage(m_damage);
        }
    }


}