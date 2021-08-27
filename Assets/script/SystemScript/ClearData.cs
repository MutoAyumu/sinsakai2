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
        var Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        m_PlayerScore.text = $"あなたのスコア:{Gmanager.m_score}";
        m_PlayerTime.text = $"あなたのタイム:{(int)Gmanager.m_gameTimer}";
    }
}
