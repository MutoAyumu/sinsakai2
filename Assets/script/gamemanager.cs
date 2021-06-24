using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance = null;
    [SerializeField] GameObject Respawn = default;
    public int lifeNum;
    public int objectNum;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //public void Instantiate()
    //{
    //    Instantiate(Respawn, new Vector2(-7, 1), Quaternion.identity);
    //}
}
