using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject m_endPanel = default;
    [SerializeField] GameObject m_instancePrefab = default;

    gamemanager Gmanager;

    // Start is called before the first frame update
    void Start()
    {
        Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        Gmanager.m_end = false;
        Gmanager.m_gameSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gmanager.m_end)
        {
            if (m_instancePrefab == null)
            {
                StartCoroutine("End");
            }
        }
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        m_instancePrefab = GameObject.Instantiate(m_endPanel);
    }
}
