using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsSave : MonoBehaviour
{
    public float m_highScore1 = PlayerPrefs.GetFloat("score1",0);
    public float m_highScore2 = PlayerPrefs.GetFloat("score2",0);
    public float m_highScore3 = PlayerPrefs.GetFloat("score3",0);

    private void Awake()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var Smanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        if (m_highScore1 < Smanager.m_score)
        {
            m_highScore1 = Smanager.m_score;
            PlayerPrefs.SetFloat("score1", m_highScore1);
        }
        else if(m_highScore2 < Smanager.m_score)
        {
            m_highScore2 = Smanager.m_score;
            PlayerPrefs.SetFloat("score2", m_highScore2);
        }
        else
        {
            m_highScore3 = Smanager.m_score;
            PlayerPrefs.SetFloat("score3", m_highScore3);
        }
    }
}
