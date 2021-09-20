using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : ItemBase
{
    [SerializeField] int m_addLife = 1;

    public override void Activate()
    {
        FindObjectOfType<PlayerHealth>().AddLife(m_addLife);
    }
}
