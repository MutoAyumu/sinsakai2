using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int max_hp;
    public int currenHp;
    int maxvalue;

    public Slider slider;
    Slider destroySlider;

    gamemanager Gmanager;
    // Start is called before the first frame update
    void Start()
    {
        Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        maxvalue = (int)slider.maxValue;
        slider.value = 1f;
        currenHp = max_hp;
        Debug.Log("同じにする");
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
        Debug.Log("hit");
        currenHp -= damage;

        if (currenHp > 0)
        {
            DOVirtual.Float(slider.value, (float)currenHp / max_hp, 0.5f, value => slider.value = value);
        }
        else
        {
            
            //currenHp = 0;
            //slider.value = 0;
            
            Gmanager.lifeNum--;
            

            if (Gmanager.lifeNum >= 0)
            {
                transform.position = new Vector2(-7, 1);
                //Gmanager.Instantiate();
                slider.value = 1f;
                currenHp = max_hp;
            }
            else
            {
                slider.value = 0f;
                Destroy(gameObject);
            }
        }
    }

    
    void Update()
    {

    }
}
