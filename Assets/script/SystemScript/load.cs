using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSceneEnd()
    {
        SceneManager.LoadScene("EndScene");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void LoadSceneStart()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadRestart()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }
}
