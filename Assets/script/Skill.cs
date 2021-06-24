using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    Transform Player;

    private void Start()
    {
        Player = GameObject.Find("player").GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            Player.transform.position = new Vector2(other.transform.position.x, transform.position.y + 1.5f);
        }
        Destroy(this.gameObject, 0.25f);
    }
}
