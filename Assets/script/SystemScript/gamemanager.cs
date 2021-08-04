using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance = null;
    public int lifeNum;
    public int m_playerHealth;
    public float m_currenthp;
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
