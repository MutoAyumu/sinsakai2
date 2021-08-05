using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    //[SerializeField] public int max_hp;
    [SerializeField] float currenHp;
    int m_maxhp;
    int maxvalue;

    public Slider slider;
    Slider destroySlider;

    gamemanager Gmanager;

    Transform ResPos = default;

    PlayerState m_state = PlayerState.Normal;
    [SerializeField] PlayerMove m_player;
    float m_jumpState;
    float m_moveState;
    float m_webJump;
    float m_webMove;
    float m_damage;
    bool m_poisoned = false;
    bool m_spiderweb = false;
    bool m_confusion = false;

    void Start()
    {
        ResPos = GameObject.Find("RespawnPoint").GetComponent<Transform>();
        this.transform.position = ResPos.transform.position;
        Gmanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        maxvalue = (int)slider.maxValue;
        m_maxhp = Gmanager.m_playerHealth;
        currenHp = Gmanager.m_currenthp;
        slider.value = (float)currenHp / m_maxhp;
        m_jumpState = m_player.m_jumppower;
        m_moveState = m_player.m_movepower;
        m_webJump = m_jumpState / 2;
        m_webMove = m_moveState / 2;
    }


    /// <summary>ダメージを受ける関数</summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        if (!Gmanager.m_godmode)
        {
            currenHp -= damage;
            Gmanager.m_currenthp = currenHp;

            if (currenHp > 0)
            {
                DOVirtual.Float(slider.value, (float)currenHp / m_maxhp, 0.5f, value => slider.value = value);
            }
            else
            {
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

    enum PlayerState
    {
        Normal,
        Spiderweb,
        Poisoned,
        Confusion,
    }
    IEnumerator StateChangeWeb()
    {
        if (m_spiderweb)
        {
            m_player.m_movepower = m_webMove;
            m_player.m_jumppower = m_webJump;
        }
        yield return new WaitForSeconds(5f);
        m_spiderweb = false;
        m_player.m_movepower = m_moveState;
        m_player.m_jumppower = m_jumpState;
        m_state = PlayerState.Normal;
    }
    IEnumerator StateChangePoisoned()
    {
        if (m_poisoned)
        {
            TakeDamage(0.1f * Time.deltaTime);
        }
        yield return new WaitForSeconds(5f);
        m_poisoned = false;
        m_state = PlayerState.Normal;
    }
    IEnumerator StartChangeConfusion()
    {
        m_player.m_playerDirection = -1f;
        yield return new WaitForSeconds(5f);
        m_confusion = false;
        m_player.m_playerDirection = 1f;
        m_state = PlayerState.Normal;
    }
    void Update()
    {
        switch (m_state)
        {
            case PlayerState.Normal:
                break;
            case PlayerState.Poisoned:
                m_poisoned = true;
                break;
            case PlayerState.Spiderweb:
                m_spiderweb = true;
                break;
            case PlayerState.Confusion:
                m_confusion = true;
                break;
            default:
                Debug.LogError("不正な状態");
                break;
        }

        if(m_poisoned)
        {
            StartCoroutine("StateChangePoisoned");

        }
        if (m_spiderweb)
        {
            StartCoroutine("StateChangeWeb");

        }
        if(m_confusion)
        {
            StartCoroutine("StartChangeConfusion");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spiderweb")
        {
            m_state = PlayerState.Spiderweb;
        }
        if (collision.gameObject.tag == "Poisoned")
        {
            m_state = PlayerState.Poisoned;
        }
        if(collision.gameObject.tag == "Confusion")
        {
            m_state = PlayerState.Confusion;
        }
    }
}
