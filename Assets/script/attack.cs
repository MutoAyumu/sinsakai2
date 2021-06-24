using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    
    [SerializeField] GameObject m_attack = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mtf = this.transform.position;
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(m_attack, new Vector2(mtf.x + 2,mtf.y), this.transform.rotation);

        }
    }
}
