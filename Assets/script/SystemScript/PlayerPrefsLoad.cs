using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsLoad : MonoBehaviour
{
    [SerializeField] Text m_highScoreMessage1 = default;
    [SerializeField] Text m_highScoreMessage2 = default;
    [SerializeField] Text m_highScoreMessage3 = default;

    public float m_highScore1 = 0;
    public float m_highScore2 = 0;
    public float m_highScore3 = 0;

    private void Start()
    {
        ScoreLoad();
    }

    void ScoreLoad()
    {
        m_highScore1 = PlayerPrefs.GetFloat("score1", 0);
        m_highScore2 = PlayerPrefs.GetFloat("score2", 0);
        m_highScore3 = PlayerPrefs.GetFloat("score3", 0);

        var Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();

        if (m_highScore1 < Gmanager.m_score)
        {
            m_highScore1 = Gmanager.m_score;
            PlayerPrefs.SetFloat("score1", m_highScore1);
        }
        else if (m_highScore2 < Gmanager.m_score)
        {
            m_highScore2 = Gmanager.m_score;
            PlayerPrefs.SetFloat("score2", m_highScore2);
        }
        else
        {
            m_highScore3 = Gmanager.m_score;
            PlayerPrefs.SetFloat("score3", m_highScore3);
        }

        if (m_highScoreMessage1 && m_highScoreMessage2 && m_highScoreMessage3)
        {


            m_highScoreMessage1.text = $"１位:{m_highScore1}";
            m_highScoreMessage2.text = $"２位:{m_highScore2}";
            m_highScoreMessage3.text = $"３位:{m_highScore3}";
        }
    }
}
