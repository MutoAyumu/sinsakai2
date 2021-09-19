using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour, IPause
{
    public static ScoreManager instance = null;


    [SerializeField] float m_minutes = 5;
    public float m_gameTimer;

    [SerializeField] Text m_timeText;

    public float m_score = 0;
    [SerializeField] Text m_scoreText = default;

    [HideInInspector] public bool m_gameSet = false;
    public bool m_end = false;

    [HideInInspector] public bool isStop = false;

    private void Awake()
    {
        m_gameTimer = m_minutes * 60;
        m_scoreText.text = "Score : " + m_score.ToString();

        if (instance != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            instance = this;
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
            m_timeText.text = "Time:" + ((int)m_gameTimer).ToString();
        }
    }

    void Timer()
    {
        if (m_timeText && !isStop)
        {
            m_gameTimer -= Time.deltaTime;
        }
    }
    public void Score(float score)
    {
        if (m_scoreText && !isStop)
        {
            m_score += score;
            m_scoreText.text = "Score:" + m_score.ToString();
        }
    }
    void IPause.Pause()
    {
        isStop = true;
    }
    void IPause.Resume()
    {
        isStop = false;
    }
}
