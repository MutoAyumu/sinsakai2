using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScore : MonoBehaviour
{
    [SerializeField] int m_score = 100;
    ScoreManager Smanager;

    void Start()
    {
        Smanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnDestroy()
    {     
        Smanager.Score(m_score);

        if(this.gameObject.name == "BossEnemy")
        {
            Smanager.m_gameSet = true;
        }
    }
}
