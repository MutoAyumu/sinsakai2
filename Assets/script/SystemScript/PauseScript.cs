using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject m_pausePrefab;
    [SerializeField] GameObject m_instancePrefab;

    private void Update()
    {
        if(Input.GetButtonDown("tab"))
        {
            if(m_instancePrefab == null)
            {
                m_instancePrefab = GameObject.Instantiate(m_pausePrefab);
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(m_instancePrefab);
                Time.timeScale = 1f;
            }
        }
    }
}
