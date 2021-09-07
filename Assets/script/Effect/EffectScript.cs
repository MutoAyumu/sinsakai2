using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
