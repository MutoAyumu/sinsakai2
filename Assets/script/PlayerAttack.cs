using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public int _damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var hit = other.gameObject;
        var hp = hit.GetComponent<EnemyHealth>();
        Debug.Log("a");
        if (other.gameObject.tag == "enemy")
        {
            //Destroy(other.gameObject);
            hp.TakeDamage(_damage);
            //Destroy(this.gameObject, 0.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 0.25f);
    }
}
