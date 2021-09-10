using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject m_endPanel = default;
    [SerializeField] GameObject m_instancePrefab = default;

    ScoreManager Smanager;

    // Start is called before the first frame update
    void Start()
    {
        Smanager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        Smanager.m_end = false;
        Smanager.m_gameSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Smanager.m_end)
        {
            if (m_instancePrefab == null)
            {
                m_instancePrefab = GameObject.Instantiate(m_endPanel);
                //StartCoroutine("End");
            }
        }
    }
    //IEnumerator End()
    //{
    //    yield return new WaitForSeconds(2);
    //    m_instancePrefab = GameObject.Instantiate(m_endPanel);
    //}
}
