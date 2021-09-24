using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas m_gameOverCanvas = default;
    [SerializeField] ParticleSystem m_gameOverEffect = default;
    ScoreManager m_scoreManager;
    GameObject m_player;
    [HideInInspector] public bool isOver;

    private void Start()
    {
        m_scoreManager = FindObjectOfType<ScoreManager>();     
    }

    private void Update()
    {
        if(m_scoreManager.m_gameTimer <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (!isOver)
        {
            m_player = GameObject.Find("Playerbox");
            m_gameOverCanvas.gameObject.SetActive(true);
            m_scoreManager.isStop = true;
            Instantiate(m_gameOverEffect, m_player.transform.position, Quaternion.identity);
            Destroy(m_player);
            isOver = true;
        }
    }
}
