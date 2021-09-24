using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{

    ScoreManager Smanager = default;

    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    int[] rankingValue = new int[5];

    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[5];

    // Use this for initialization
    void Start()
    {
        Smanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        GetRanking();

        SetRanking(Smanager.m_score);

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = $"#{i+1}:{rankingValue[i]}";
        }
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    void SetRanking(float score)
    {
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (score > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = (int)score;
                score = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

