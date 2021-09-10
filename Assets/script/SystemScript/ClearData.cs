using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearData : MonoBehaviour
{
    [SerializeField] Text m_PlayerScore = default;
    [SerializeField] Text m_PlayerTime = default;

    private void Start()
    {
        var Smanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        m_PlayerScore.text = $"あなたのスコア:{Smanager.m_score}";
        m_PlayerTime.text = $"あなたのタイム:{(int)Smanager.m_gameTimer}";
    }
}
