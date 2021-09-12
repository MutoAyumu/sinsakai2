using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject m_pausePrefab;
    [SerializeField] GameObject m_instancePrefab;

    /// <summary>true の時は一時停止とする</summary>
    bool m_pauseFlg = false;

    void Update()
    {
        // ESC キーが押されたら一時停止・再開を切り替える
        if (Input.GetButtonDown("tab"))
        {
            PauseResume();
            if (m_instancePrefab == null)
            {
                m_instancePrefab = GameObject.Instantiate(m_pausePrefab);
            }
            else
            {
                Destroy(m_instancePrefab);
            }
        }
    }

    /// <summary>
    /// 一時停止・再開を切り替える
    /// </summary>
    void PauseResume()
    {
        m_pauseFlg = !m_pauseFlg;

        // 全ての GameObject を取ってきて、IPause を継承したコンポーネントが追加されていたら Pause または Resume を呼ぶ
        var objects = FindObjectsOfType<GameObject>();

        foreach (var o in objects)
        {
            IPause i = o.GetComponent<IPause>();

            if (m_pauseFlg)
            {
                i?.Pause();     // ここで「多態性」が使われている
            }
            else
            {
                i?.Resume();    // ここで「多態性」が使われている
            }
        }
    }
}
