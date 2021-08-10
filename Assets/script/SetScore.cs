using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScore : MonoBehaviour
{
    [SerializeField] float m_score = 100f;
    gamemanager Gmanager;

    void Start()
    {
        Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
    }

    private void OnDestroy()
    {     
        Gmanager.Score(m_score);
    }
}
