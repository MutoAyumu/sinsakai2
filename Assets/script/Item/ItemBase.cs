using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class ItemBase : MonoBehaviour, IPause
{
    /// <summary>アイテムを取った時に鳴る効果音</summary>
    [Tooltip("アイテムを取った時に鳴らす効果音")]
    [SerializeField] AudioClip m_sound = default;
    /// <summary>アイテムの効果をいつ発揮するか</summary>
    [Tooltip("Get を選ぶと、取った時に効果が発動する。Use を選ぶと、アイテムを使った時に発動する")]
    [SerializeField] ActivateTiming m_whenActivated = ActivateTiming.Get;

    Animator m_anim;

    private void Start()
    {
        m_anim = this.gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// アイテムが発動する効果を実装する
    /// </summary>
    public abstract void Activate();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            if (m_sound)
            {
                AudioSource.PlayClipAtPoint(m_sound, Camera.main.transform.position);
            }

            // アイテム発動タイミングによって処理を分ける
            if (m_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            else if (m_whenActivated == ActivateTiming.Use)
            {
                // 見えない所に移動する
                this.transform.position = Camera.main.transform.position;
                // コライダーを無効にする
                GetComponent<Collider2D>().enabled = false;
                // プレイヤーにアイテムを渡す
                collision.gameObject.GetComponent<PlayerMove>().GetItem(this);
            }
        }
    }

    /// <summary>
    /// アイテムをいつアクティベートするか
    /// </summary>
    enum ActivateTiming
    {
        /// <summary>取った時にすぐ使う</summary>
        Get,
        /// <summary>「使う」コマンドで使う</summary>
        Use,
    }

    void IPause.Pause()
    {
        m_anim.speed = 0;
    }
    void IPause.Resume()
    {
        m_anim.speed = 1;
    }
}
