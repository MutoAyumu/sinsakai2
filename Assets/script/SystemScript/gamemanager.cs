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

    private void Awake()
    {
        m_currenthp = m_playerHealth;

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
}
