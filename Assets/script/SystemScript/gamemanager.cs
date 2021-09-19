using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text m_gameOverText = default;
    [SerializeField] ParticleSystem m_gameOverEffect = default;
    ScoreManager m_scoreManager;
    GameObject m_player;
    bool isOver;

    private void Start()
    {
        m_scoreManager = FindObjectOfType<ScoreManager>();
        m_player = GameObject.Find("Playerbox");
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
            m_gameOverText.gameObject.SetActive(true);
            m_scoreManager.isStop = true;
            Instantiate(m_gameOverEffect, m_player.transform.position, Quaternion.identity);
            Destroy(m_player);
            isOver = true;
        }
    }
}
