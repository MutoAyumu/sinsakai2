using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    [SerializeField] public int _damage = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        var hp = hit.GetComponent<PlayerHealth>();
        
        if(hp != null)
        {
            hp.TakeDamage(_damage);
        }
    }
}
