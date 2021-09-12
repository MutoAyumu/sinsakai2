using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionScript : MonoBehaviour
{
    [SerializeField] GameObject m_selectPrefab;
    [SerializeField] GameObject m_instancePrefab;

    public void Option()
    {
        var before = GameObject.Find("Option(Clone)");
        var m_currentSystem = GameObject.FindObjectOfType<EventSystemController>();

        if (m_instancePrefab == null)
        {
            m_currentSystem.m_eventSystem.enabled = false;
            m_instancePrefab = GameObject.Instantiate(m_selectPrefab);
        }

        if (before)
        {
            Destroy(before);
        }
    }
    public void Back()
    {
        var m_currentSystem = GameObject.FindObjectOfType<EventSystemController>();

        if (m_instancePrefab == null && this.gameObject.transform.parent.gameObject.name != "Option(Clone)")
        {
            m_instancePrefab = GameObject.Instantiate(m_selectPrefab);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
        else
        {
            m_currentSystem.m_eventSystem.enabled = true;
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
