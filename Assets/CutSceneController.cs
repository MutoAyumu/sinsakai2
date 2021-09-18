using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour , IPause
{
    [SerializeField] UnityEvent m_eventOnCutSceneEnds = default;
    private PlayableDirector m_playableDirector = default;

    private void Start()
    {
        m_playableDirector = this.gameObject.GetComponent<PlayableDirector>();
    }

    public void OnCutSceneEnd()
    {
        m_eventOnCutSceneEnds.Invoke();
    }
    void IPause.Pause()
    {
        m_playableDirector.Pause();
    }
    void IPause.Resume()
    {
        m_playableDirector.Resume();
    }
}
