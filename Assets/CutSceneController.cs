using UnityEngine;
using UnityEngine.Events;

public class CutSceneController : MonoBehaviour
{
    [SerializeField] UnityEvent m_eventOnCutSceneEnds = default;

    public void OnCutSceneEnd()
    {
        m_eventOnCutSceneEnds.Invoke();
    }
}
