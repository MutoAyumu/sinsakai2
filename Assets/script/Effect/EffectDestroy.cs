using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
