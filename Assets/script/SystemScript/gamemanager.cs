using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance = null;
    [SerializeField] public int lifeNum;
    [SerializeField] public int m_playerHealth;
    [SerializeField] public float m_currenthp;
    [SerializeField] public bool m_godmode = false;

    [SerializeField] int m_minutes = 5;
    public float m_gameTimer;

    [SerializeField] Text m_timeText;

    public float m_score = 0;
    [SerializeField] Text m_scoreText = default;

    [HideInInspector] public bool m_gameSet = false;
    public bool m_end = false;

    private void Awake()
    {
        m_currenthp = m_playerHealth;
        m_gameTimer = m_minutes * 60;
        m_scoreText.text = "スコア : " + m_score.ToString();

        if (instance != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (!m_gameSet)
        {
            Timer();
        }
        else if (!m_end)
        {
            Score((int)m_gameTimer * 10);
            m_end = true;
        }

        if (m_timeText)
        {
            m_timeText.text = "残り時間 : " + ((int)m_gameTimer).ToString();      
        }
    }  

    void Timer()
    {
        if (m_timeText)
        {
            m_gameTimer -= Time.deltaTime;
        }
    }
    public void Score(float score)
    {
        if (m_scoreText)
        {
            m_score += score;
            m_scoreText.text = "スコア : " + m_score.ToString();
        }
    }
}
