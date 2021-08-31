using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI を操作するために追加している

/// <summary>
/// 残機表示を行うコンポーネント
/// 管理オブジェクトにアタッチして使う
/// </summary>
public class PlayerCounter : MonoBehaviour
{
    /// <summary>残機として表示するスプライト</summary>
    [SerializeField] Sprite m_playerUiSprite = null;
    /// <summary>残機の下に配置するスプライト</summary>
    [SerializeField] Sprite m_playerUiBase = null;
    /// <summary>残機として表示するスプライトのサイズ</summary>
    [SerializeField] Vector2 m_spriteSize = new Vector2(50f, 50f);
    /// <summary>残機表示をするパネル</summary>
    [SerializeField] RectTransform m_playerCounterPanel = null;
    /// <summary>残機の下に表示するパネル</summary>
    [SerializeField] RectTransform m_playerCounterBase = null;

    /// <summary>
    /// 残機表示を更新する
    /// </summary>
    /// <param name="playerCount">残機数</param>
    public void Refresh(float playerCount)
    {
        if (m_playerUiSprite && m_playerCounterPanel)
        {
            // 子オブジェクトをすべて削除する
            foreach (Transform t in m_playerCounterPanel.transform)
            {
                Destroy(t.gameObject);
            }

            // 残機数だけスプライトをパネルの子オブジェクトとして生成する
            for (int i = 0; i < playerCount; i++)
            {
                // Image を作る
                GameObject go = new GameObject();
                Image image = go.AddComponent<Image>();
                // Sprite をアサインする
                image.sprite = m_playerUiSprite;
                // サイズを変える
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = m_spriteSize;
                // パネルの子オブジェクトにする
                go.transform.SetParent(m_playerCounterPanel.transform);
            }
        }
    }
    public void Set(float playerBase)
    {
        if (m_playerUiBase && m_playerCounterBase)
        {
            // 子オブジェクトをすべて削除する
            foreach (Transform t in m_playerCounterBase.transform)
            {
                Destroy(t.gameObject);
            }

            // 残機数だけスプライトをパネルの子オブジェクトとして生成する
            for (int i = 0; i < playerBase; i++)
            {
                // Image を作る
                GameObject go = new GameObject();
                Image image = go.AddComponent<Image>();
                // Sprite をアサインする
                image.sprite = m_playerUiBase;
                // サイズを変える
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = m_spriteSize;
                // パネルの子オブジェクトにする
                go.transform.SetParent(m_playerCounterBase.transform);
            }
        }
    }
}