using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : ItemBase
{
    [SerializeField] int m_addScere = 500;

    public override void Activate()
    {
        FindObjectOfType<ScoreManager>().Score(m_addScere);
    }
}
