using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    //[SerializeField] public int max_hp;
    [SerializeField] int currenHp;
    int m_maxhp;
    int maxvalue;

    public Slider slider;
    Slider destroySlider;

    gamemanager Gmanager;

    Transform ResPos = null;

    // Start is called before the first frame update
    void Start()
    {
        ResPos = GameObject.Find("RespawnPoint").GetComponent<Transform>();
        this.transform.position = ResPos.transform.position;
        Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        maxvalue = (int)slider.maxValue;
        m_maxhp = Gmanager.m_playerHealth;
        currenHp = Gmanager.m_currenthp;
        slider.value = (float)currenHp / m_maxhp;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "enemy")
    //    {

    //    }
    //}

    //ダメージ処理
    public void TakeDamage(int damage)
    {
        if (!Gmanager.m_godmode)
        {
            Debug.Log("hit");
            currenHp -= damage;
            Gmanager.m_currenthp = currenHp;

            if (currenHp > 0)
            {
                DOVirtual.Float(slider.value, (float)currenHp / m_maxhp, 0.5f, value => slider.value = value);
            }
            else
            {

                //currenHp = 0;
                //slider.value = 0;

                Gmanager.lifeNum--;


                if (Gmanager.lifeNum >= 0)
                {
                    transform.position = ResPos.transform.position;
                    slider.value = 1f;
                    currenHp = m_maxhp;
                    Gmanager.m_currenthp = m_maxhp;
                }
                else
                {
                    slider.value = 0f;
                    Destroy(gameObject);
                }
            }
        }
    }

    
    void Update()
    {

    }
}
